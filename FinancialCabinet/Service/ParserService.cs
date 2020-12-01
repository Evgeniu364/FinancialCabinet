using AngleSharp;
using AngleSharp.Dom;
using FinancialCabinet.Entity;
using FinancialCabinet.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinancialCabinet.Service
{

    // todo: Set up periodic banks parsing and saving to the database (parser is not used anywhere now)

    public class ParserService
    {
        public List<Bank> ParseBanks()
        {
            Console.WriteLine("Start parsing banks... ");
            IConfiguration config = Configuration.Default.WithDefaultLoader();
            IBrowsingContext context = BrowsingContext.New(config);
            //ParseCredits(context, "https://myfin.by/bank/belarusbank");
            IDocument document = context.OpenAsync("https://myfin.by/banki").Result;
            IElement banksTable = document.QuerySelectorAll("table[class*='rates-table-sort']").First();
            List<Bank> bankList = new List<Bank>();
            IHtmlCollection<IElement> banksHtml = banksTable.QuerySelectorAll("tr[class*='tr-tb']");
            int i = 0;
            foreach (IElement bankRow in banksHtml)
            {
                i++;
                Console.WriteLine($"{i}/{banksHtml.Count()}");
                string bankName;
                string bankAddress;
                string bankURL;
                List<Phone> bankPhoneNumbers = new List<Phone>();
                IHtmlCollection<IElement> currentBankColumns = bankRow.QuerySelectorAll("td");
                bankName = currentBankColumns[0].QuerySelector("span").InnerHtml;
                bankURL = "https://myfin.by" + currentBankColumns[0].QuerySelector("a").GetAttribute("href");
                foreach (IElement phoneNumberElement in currentBankColumns[1].QuerySelectorAll("a[class*='phone']"))
                {
                    bankPhoneNumbers.Add(new Phone { PhoneNumber = phoneNumberElement.InnerHtml.Replace("+", "").Replace("-", "").Replace(" ", "") });
                }
                bankAddress = currentBankColumns[2].TextContent.Replace("\n", "").Trim();
                Bank bank = new Bank
                {
                    ID = Guid.NewGuid(),
                    Name = bankName,
                    PhoneList = bankPhoneNumbers,
                    Address = bankAddress,
                    DepositList = ParseDeposits(context, bankURL),
                    CreditList = ParseCredits(context, bankURL)
                };
                bankList.Add(bank);
                //return bankList;
            }
            context.Dispose();
            document.Dispose();
            Console.WriteLine("complete");
            return bankList;
            //return null;
        }

        private List<Deposit> ParseDeposits(IBrowsingContext context, string bankURL)
        {
            string depositsURL = bankURL + "/vklady";
            IDocument depositsDocument = context.OpenAsync(depositsURL).Result;
            IHtmlCollection<IElement> depositDivList = depositsDocument.QuerySelector("div[class=credit-rates-table__body]").QuerySelectorAll("div[class='credit-rates-table__row']");//.QuerySelectorAll("div[class='credit-rates-table__body']");
            List<Deposit> depositList = new List<Deposit>();
            foreach (IElement depositDiv in depositDivList)
            {
                string depositName = "";
                string depositDetailsURL = "";
                SingleDeposit singleDeposit = new SingleDeposit();
                Deposit deposit = new Deposit { ID = Guid.NewGuid(), SingleDepositList = new List<SingleDeposit>() };
                foreach (IElement currentDeposit in depositDiv.QuerySelectorAll("div[class='credit-rates-table__content']"))
                {
                    IHtmlCollection<IElement> depositData = currentDeposit.QuerySelectorAll("div[class='credit-rates-table__cell']");
                    depositName = depositData[0].QuerySelectorAll("a").Length == 1 ? depositData[0].QuerySelector("a").TextContent : depositName;
                    depositDetailsURL = depositData[0].QuerySelectorAll("a").Length == 1 ? "https://myfin.by" + depositData[0].QuerySelector("a").GetAttribute("href") : depositDetailsURL;
                    string currency = depositData[1].QuerySelector("span[class='credit-rates-table__value']").TextContent;
                    string percentString = depositData[2].QuerySelector("span[class='credit-rates-table__value accent']").TextContent;
                    string periodString = depositData[3].QuerySelector("span[class='credit-rates-table__value']").TextContent;
                    string sumString = depositData[4].QuerySelector("span[class='credit-rates-table__value']").TextContent;
                    singleDeposit = new SingleDeposit { ID = Guid.NewGuid() };
                    Period depositPeriod = new Period();
                    Percent depositPercent = new Percent();
                    if (percentString.Contains("до"))
                    {
                        depositPercent.MinPercent = double.Parse(percentString.Split("до")[0].Replace("от", "").Replace("%", "").Trim(), CultureInfo.InvariantCulture);
                        depositPercent.MaxPercent = double.Parse(percentString.Split("до")[1].Replace("%", "").Trim(), CultureInfo.InvariantCulture);
                        depositPercent.IsInterval = true;
                    }
                    else
                    {
                        depositPercent.MaxPercent = double.Parse(percentString.Replace("%", "").Trim(), CultureInfo.InvariantCulture);
                        depositPercent.IsInterval = false;
                    }
                    if (periodString.Contains("до"))
                    {
                        string[] splittedPeriod = periodString.Split("до");
                        depositPeriod.MinPeriod = int.Parse(splittedPeriod[0].Replace("от", "").Replace("мес.", "").Replace("дн.", "").Trim());
                        depositPeriod.MinPeriodType = splittedPeriod[0].Contains("дн.") ? PeriodTypeEnum.Day : PeriodTypeEnum.Month;
                        depositPeriod.MaxPeriod = int.Parse(splittedPeriod[1].Replace("до", "").Replace("мес.", "").Replace("дн.", "").Trim());
                        depositPeriod.MaxPeriodType = splittedPeriod[1].Contains("дн.") ? PeriodTypeEnum.Day : PeriodTypeEnum.Month;
                        depositPeriod.IsInterval = true;
                    }
                    else
                    {
                        depositPeriod.MaxPeriod = int.Parse(periodString.Replace("от", "").Replace("мес.", "").Replace("дн.", "").Trim());
                        depositPeriod.MaxPeriodType = periodString.Contains("дн.") ? PeriodTypeEnum.Day : PeriodTypeEnum.Month;
                        depositPeriod.IsInterval = false;
                    }
                    try
                    {
                        singleDeposit.Sum = int.Parse(Regex.Matches(sumString, @"[0-9]+")[0].ToString());
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        // Данных о сумме нет
                        singleDeposit.Sum = null;
                    }
                    IDocument singleDepositDetails = context.OpenAsync(depositDetailsURL).Result;
                    string singleDepositDetailsBlock = singleDepositDetails.QuerySelectorAll("div[class='deposit-short-info__info-block']")[1].InnerHtml;
                    singleDeposit.Currency = currency;
                    singleDeposit.Percent = depositPercent;
                    singleDeposit.Period = depositPeriod;
                    singleDeposit.IsRevocable = !singleDepositDetailsBlock.Contains("Безотзывной");
                    singleDeposit.IsReplenishable = singleDepositDetailsBlock.Contains("Есть пополнения");
                    deposit.SingleDepositList.Add(singleDeposit);

                }
                Console.WriteLine("Deposit: " + depositName);
                deposit.DepositName = depositName;
                depositList.Add(deposit);
            }
            depositsDocument.Dispose();
            return depositList;
        }

        private List<Credit> ParseCredits(IBrowsingContext context, string bankURL)
        {
            string depositsURL = bankURL + "/kredity";
            IDocument creditsDocument = context.OpenAsync(depositsURL).Result;
            IHtmlCollection<IElement> creditDivList;
            try
            {
                creditDivList = creditsDocument.QuerySelector("div[class='credit-rates']").QuerySelectorAll("div[class='table-responsive']")[0].QuerySelector("tbody").QuerySelectorAll("tr");
            }
            catch (Exception)
            {
                return null;
            }
            List<Credit> creditList = new List<Credit>();
            Credit credit = new Credit { ID = Guid.NewGuid(), SingleCreditList = new List<SingleCredit>() };
            bool isFirstCredit = true;
            string lastCreditName = "";
            foreach (IElement creditElement in creditDivList)
            {
                if (creditElement.InnerHtml.Contains("<td colspan=\"6\">"))
                    continue;
                bool isNewGroup = creditElement.QuerySelectorAll("td").Length == 6;
                int currencyPos = isNewGroup ? 1 : 0;
                int sumPos = isNewGroup ? 2 : 1;
                int periodPos = isNewGroup ? 3 : 2;
                int percentPos = isNewGroup ? 4 : 3;
                int detailsPos = isNewGroup ? 5 : 4;
                IHtmlCollection<IElement> creditInfoElement = creditElement.QuerySelectorAll("td");
                string currency = creditInfoElement[currencyPos].TextContent;
                string sumString = creditInfoElement[sumPos].TextContent.Replace(" ", "");
                string periodString = creditInfoElement[periodPos].TextContent;
                string percentString = creditInfoElement[percentPos].TextContent;
                string creditDetailsURL = "";
                string creditName = "";
                bool isGuarantorNeeded = true;
                bool isIncomeCertificationNeeded = true;
                if (isNewGroup)
                {

                    creditName = creditInfoElement[0].QuerySelector("a").InnerHtml;
                    creditDetailsURL = "https://myfin.by" + creditInfoElement[0].QuerySelector("a").GetAttribute("href");
                    if (isFirstCredit)
                        credit.CreditName = creditName;
                }
                int minSum = 0;
                int maxSum = 0;
                Period creditPeriod = new Period();
                Percent creditPercent = new Percent();
                SingleCredit singleCredit = new SingleCredit { ID = Guid.NewGuid(), Currency = currency };
                if (sumString.Contains("от"))
                    minSum = int.Parse(Regex.Matches(sumString, @"[\d]+")[0].Value);
                if (sumString.Contains("до") && sumString.Contains("от"))
                    maxSum = int.Parse(Regex.Matches(sumString, @"[\d]+")[1].Value);
                else if (sumString.Contains("до"))
                    maxSum = int.Parse(Regex.Matches(sumString, @"[\d]+")[0].Value);
                if (periodString.Contains("до"))
                {
                    string[] splittedPeriod = periodString.Split("до");
                    creditPeriod.MinPeriod = int.Parse(splittedPeriod[0].Replace("от", "").Replace("мес.", "").Replace("дн.", "").Trim());
                    creditPeriod.MaxPeriod = int.Parse(splittedPeriod[1].Replace("до", "").Replace("мес.", "").Replace("дн.", "").Trim());
                    creditPeriod.MaxPeriodType = splittedPeriod[1].Contains("дн.") ? PeriodTypeEnum.Day : PeriodTypeEnum.Month;
                    creditPeriod.MinPeriodType = splittedPeriod[0].Contains("дн.") ? PeriodTypeEnum.Day : splittedPeriod[0].Contains("мес.") ? PeriodTypeEnum.Month : creditPeriod.MaxPeriodType;
                    creditPeriod.IsInterval = true;
                }
                else
                {
                    creditPeriod.MaxPeriod = int.Parse(periodString.Replace("от", "").Replace("мес.", "").Replace("дн.", "").Trim());
                    creditPeriod.MaxPeriodType = periodString.Contains("дн.") ? PeriodTypeEnum.Day : PeriodTypeEnum.Month;
                    creditPeriod.IsInterval = false;
                }
                if (percentString.Contains("до"))
                {
                    if (percentString.Contains("от"))
                    {
                        creditPercent.MinPercent = double.Parse(percentString.Split("до")[0].Replace("от", "").Replace("%", "").Trim(), CultureInfo.InvariantCulture);
                        creditPercent.MaxPercent = double.Parse(percentString.Split("до")[1].Replace("%", "").Trim(), CultureInfo.InvariantCulture);
                        creditPercent.IsInterval = true;
                    } else
                    {
                        creditPercent.MaxPercent = double.Parse(percentString.Replace("до", "").Replace("%", "").Trim(), CultureInfo.InvariantCulture);
                        creditPercent.IsInterval = false;
                    }
                }
                else
                {
                    try
                    {
                        creditPercent.MaxPercent = double.Parse(percentString.Replace("%", "").Trim(), CultureInfo.InvariantCulture);
                    } catch (Exception)
                    {
                        creditPercent.MaxPercent = 0;
                    }
                    creditPercent.IsInterval = false;
                }
                if (isNewGroup)
                {
                    IDocument creditDetails = context.OpenAsync(creditDetailsURL).Result;
                    creditDetails.QuerySelectorAll("tr").ToList().ForEach(element =>
                    {
                        if (element.InnerHtml.Contains("Без поручителей"))
                            isGuarantorNeeded = element.InnerHtml.Contains("Нет");
                        if (element.InnerHtml.Contains("Без справок"))
                            isIncomeCertificationNeeded = element.InnerHtml.Contains("Нет");

                    });
                }
                if (isNewGroup)
                {
                    if (!isFirstCredit)
                    {
                        creditList.Add(credit);
                        credit = new Credit { SingleCreditList = new List<SingleCredit>() };
                        credit.CreditName = creditName;
                    }
                    else
                    {
                        singleCredit.IsGuarantorNeeded = isGuarantorNeeded;
                        singleCredit.IsIncomeCertificationNeeded = isIncomeCertificationNeeded;
                        singleCredit.MinSum = minSum;
                        singleCredit.MaxSum = maxSum;
                        singleCredit.Period = creditPeriod;
                        singleCredit.Percent = creditPercent;
                        isFirstCredit = false;
                    }
                    credit.SingleCreditList.Add(singleCredit);
                    singleCredit.IsGuarantorNeeded = isGuarantorNeeded;
                    singleCredit.IsIncomeCertificationNeeded = isIncomeCertificationNeeded;
                    singleCredit.MinSum = minSum;
                    singleCredit.MaxSum = maxSum;
                    singleCredit.Period = creditPeriod;
                    singleCredit.Percent = creditPercent;
                } else
                {
                    singleCredit.IsGuarantorNeeded = credit.SingleCreditList.First().IsGuarantorNeeded;
                    singleCredit.IsIncomeCertificationNeeded = credit.SingleCreditList.First().IsIncomeCertificationNeeded;
                    singleCredit.MinSum = minSum;
                    singleCredit.MaxSum = maxSum;
                    singleCredit.Period = creditPeriod;
                    singleCredit.Percent = creditPercent;
                    credit.SingleCreditList.Add(singleCredit);
                }
                //lastCreditName = creditInfoElement[0].QuerySelector("a").InnerHtml;
                //if (isNewGroup)
                //    Console.WriteLine(creditInfoElement[0].TextContent);
                //Console.WriteLine("Валюта: " + currency);
                //Console.WriteLine("Минимальная сумма: " + minSum);
                //Console.WriteLine("Максимальная сумма: " + maxSum);
                //Console.WriteLine("Сроки:");
                //if(creditPeriod.IsInterval)
                //    Console.WriteLine("\tОт " + creditPeriod.MinPeriod + " " + creditPeriod.MinPeriodType.ToString());
                //Console.WriteLine("\tДо " + creditPeriod.MaxPeriod + " " + creditPeriod.MaxPeriodType.ToString());
                //Console.WriteLine("Проценты:");
                //if (creditPercent.IsInterval)
                //{
                //    Console.WriteLine("\tОт " + creditPercent.MinPercent + "%");
                //    Console.WriteLine("\tДо " + creditPercent.MaxPercent + "%");
                //} else
                //    Console.WriteLine("\t" + creditPercent.MaxPercent + "%");
                //Console.WriteLine("Нужен поручитель: " + isGuarantorNeeded.ToString());
                //Console.WriteLine("Нужны справки: " + isIncomeCertificationNeeded.ToString());
            }
            Console.WriteLine("Credit: " + credit.CreditName);
            creditList.Add(credit);
            return creditList;
        }
    }
}
