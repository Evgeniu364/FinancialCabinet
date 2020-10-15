﻿using AngleSharp;
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
            Console.Write("Start parsing banks... ");
            IConfiguration config = Configuration.Default.WithDefaultLoader();
            IBrowsingContext context = BrowsingContext.New(config);
            IDocument document = context.OpenAsync("https://myfin.by/banki").Result;
            IElement banksTable = document.QuerySelectorAll("table[class*='rates-table-sort']").First();
            List<Bank> bankList = new List<Bank>();
            foreach (IElement bankRow in banksTable.QuerySelectorAll("tr[class*='tr-tb']"))
            {
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
                    Name = bankName,
                    PhoneList = bankPhoneNumbers,
                    Address = bankAddress,
                    DepositList = ParseDeposits(context, bankURL)
                };
                bankList.Add(bank);
            }
            context.Dispose();
            document.Dispose();
            Console.WriteLine("complete");
            return bankList;
        }

        public static List<Deposit> ParseDeposits(IBrowsingContext context, string bankURL)
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
                Deposit deposit = new Deposit { SingleDepositList = new List<SingleDeposit>() };
                foreach (IElement currentDeposit in depositDiv.QuerySelectorAll("div[class='credit-rates-table__content']"))
                {
                    IHtmlCollection<IElement> depositData = currentDeposit.QuerySelectorAll("div[class='credit-rates-table__cell']");
                    depositName = depositData[0].QuerySelectorAll("a").Length == 1 ? depositData[0].QuerySelector("a").TextContent : depositName;
                    depositDetailsURL = depositData[0].QuerySelectorAll("a").Length == 1 ? "https://myfin.by" + depositData[0].QuerySelector("a").GetAttribute("href") : depositDetailsURL;
                    string currency = depositData[1].QuerySelector("span[class='credit-rates-table__value']").TextContent;
                    string percentString = depositData[2].QuerySelector("span[class='credit-rates-table__value accent']").TextContent;
                    string periodString = depositData[3].QuerySelector("span[class='credit-rates-table__value']").TextContent;
                    string sumString = depositData[4].QuerySelector("span[class='credit-rates-table__value']").TextContent;
                    singleDeposit = new SingleDeposit();
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
                depositList.Add(deposit);
            }
            depositsDocument.Dispose();
            return depositList;
        }
    }
}