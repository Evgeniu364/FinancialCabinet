using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinancialCabinet.Data;
using FinancialCabinet.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinancialCabinet.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Display(Name = "Are you individual?")]
            public bool IsIndividual { get; set; }

            [Display(Name = "First name")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
            public string Name { get; set; }

            [Display(Name = "Last name")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
            public string LastName { get; set; }

            [Display(Name = "Patronymic")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
            public string Patronymic { get; set; }

            [Display(Name = "Date of birth")]
            public DateTime? DateOfBirth { get; set; }

            [Display(Name = "Type document")]
            public string TypeDocument { get; set; }

            [Display(Name = "Document number")]
            public string DocumentNumber { get; set; }

            [Display(Name = "Salary")]
            public string Salary { get; set; }

            [Display(Name = "Company name")]
            public string CompanyName { get; set; }

            [Display(Name = "UNP")]
            public string Unp { get; set; }

            [Display(Name = "Document number")]
            public string NumberDocument { get; set; }

            [Display(Name = "Cash turnover")]
            public string CashTurnover { get; set; }

            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            [Display(Name = "Phone number")]
            public string Phone { get; set; }

            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            [Display(Name = "Address")]
            public string Address { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            LegalEntity legal = new LegalEntity();
            Individual individual = new Individual();

            if (user.IndividualID == Guid.Empty)
            {
                legal = _context.LegalEntity.Find(user.LegalEntityID);
            }
            else
            {
                individual = _context.Individuals.Find(user.IndividualID);
            }

            Input = new InputModel
            {
                Email = user.Email,
                IsIndividual = user.IndividualID == Guid.Empty ? false : true,
                Name = individual.Name,
                LastName = individual.LastName,
                Patronymic = individual.Patronymic,
                DateOfBirth = individual.DateOfBirth,
                TypeDocument = individual.TypeDocument,
                DocumentNumber = individual.NumberDocument,
                Salary = individual.Salary.ToString(),
                CompanyName = legal.CompanyName,
                Unp = legal.Unp.ToString(),
                NumberDocument = legal.NumberDocument.ToString(),
                CashTurnover = legal.CashTurnover.ToString(),
                Phone = user.Phone,
                Address = user.Address
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            LegalEntity legal = new LegalEntity();
            Individual individual = new Individual();

            if (user.IndividualID == Guid.Empty)
            {
                legal = _context.LegalEntity.Find(user.LegalEntityID);
                legal.CompanyName = Input.CompanyName;
                legal.Unp = Convert.ToInt32(Input.Unp);
                legal.NumberDocument = Convert.ToInt32(Input.NumberDocument);
                legal.CashTurnover = Convert.ToDouble(Input.CashTurnover);
                _context.Update(legal);
            }
            else
            {
                individual = _context.Individuals.Find(user.IndividualID);
                individual.Name = Input.Name;
                individual.LastName = Input.LastName;
                individual.Patronymic = Input.Patronymic;
                individual.DateOfBirth = Convert.ToDateTime(Input.DateOfBirth);
                individual.TypeDocument = Input.TypeDocument;
                individual.NumberDocument = Input.DocumentNumber;
                individual.Salary = Convert.ToDouble(Input.Salary);
                _context.Update(individual);
            }



            user.Phone = Input.Phone;
            user.Address = Input.Address;
            
            await _userManager.UpdateAsync(user);
            
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
