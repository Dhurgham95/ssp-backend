using Backend.Data;
using Backend.Models;
using Backend.PartialModels;
using Backend.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManage;
        //roles 
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;
        private readonly DataContext _db;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, DataContext db)
        {
            _userManage = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _db = db;

        }
        public async  Task<UserLoginResponse> LoginSuperAdminAsync(SuperAdminLoginViewModel model)
        {
            var user = await _db.Users.SingleOrDefaultAsync(a => a.UserName == model.SuperAdminCode);

            if (user == null)
            {
                return new UserLoginResponse
                {
                    Message = "Incorect User Name",
                    IsSuccess = false,
                };
            }
            var result = await _userManage.CheckPasswordAsync(user, model.SuperAdminPassword);


            if (!result)
            {
                return new UserLoginResponse
                {
                    Message = "Invalid Password",
                    IsSuccess = false
                };
            }


            var claims = new List<Claim> //[]
            {
                new Claim("SuperAdminCode", model.SuperAdminCode),
               // new Claim("FullName", model.),

                new Claim(ClaimTypes.NameIdentifier, user.Id),
               // new Claim(ClaimTypes.Role, userRoles)
                new Claim(ClaimTypes.Role, user.Role)
            };
            //roles 

            //   claims.Add(new Claim(ClaimTypes.Role, userRoles));



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));



            var token = new JwtSecurityToken(
                //issuer : _configuration["AuthSetings:Issuer"],
                //audience : _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserLoginResponse
            {
                // Id = user.Id,
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo,
                UserId = user.Id,
                FullName = user.UserName,
                //PhoneNumber = user.PhoneNumber,
                //AccountNumber = user.AccountNumber,
                //Email = user.Email,
                Role = user.Role,
                CreatedAt = DateTime.Now



            };



        }

        public async  Task<UserMangageResponse> RegisterSuperAdminAsync(SuperAdminRegisterViewModel model)
        {

            if (model == null)
            {
                throw new NullReferenceException("Register Model is Null");
            }
            if (model.SuperAdminPassword != model.SuperAdminConfirmPassword && model.SuperAdminSecretCodeOne != "A34RG**$%WW@TWDIOOLDSA" && model.SuperAdminSecretCodeTwo != "BR564UU%^77#EGFHDLPE__3499022JEJN00")
            {
                return new UserMangageResponse
                {
                    Message = "Wrong Register Inputs",
                    IsSuccess = false
                };
            }
            //if (IsPhoneAlreadyRegistered)
            //{
            //    return new UserMangeResponse
            //    {
            //        Message = "Phone number already exists",
            //        IsSuccess = false
            //    };
            //}

            var identityUser = new User
            {
                //Email = model.Email,
                UserName = model.SuperAdminCode,
                //PhoneNumber = model.PhoneNumber,
                //AccountNumber = model.AccountNumber,

                Role = UserRoles.SuperAdmin,
            };
            var result = await _userManage.CreateAsync(identityUser, model.SuperAdminPassword);
            if (!result.Succeeded)
            {

                return new UserMangageResponse
                {
                    Message = "User did not created",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };


            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Merchant))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Merchant));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
            {
                await _userManage.AddToRoleAsync(identityUser, UserRoles.SuperAdmin);
            }


            return new UserMangageResponse
            {

                Message = "User Created",
                IsSuccess = true

            };



        }

        public async Task<UserMangageResponse> RegisterAdminAsyc(AdminRegisterViewModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Register Model is Null");
            }
            if (model.AdminPassword != model.AdminConfirmPassword )
            {
                return new UserMangageResponse
                {
                    Message = "Wrong Register Inputs",
                    IsSuccess = false
                };
            }
            //if (IsPhoneAlreadyRegistered)
            //{
            //    return new UserMangeResponse
            //    {
            //        Message = "Phone number already exists",
            //        IsSuccess = false
            //    };
            //}

            var identityUser = new User
            {
                //Email = model.Email,
                UserName = model.AdminName,
                Branch = model.AdminBranch, 
                Department = model.AdminDepartment,
           
                //PhoneNumber = model.PhoneNumber,
                //AccountNumber = model.AccountNumber,

                Role = UserRoles.Admin,
            };
            var result = await _userManage.CreateAsync(identityUser, model.AdminPassword);
            if (!result.Succeeded)
            {

                return new UserMangageResponse
                {
                    Message = "User did not created",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };


            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Merchant))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Merchant));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManage.AddToRoleAsync(identityUser, UserRoles.Admin);
            }


            return new UserMangageResponse
            {

                Message = "User Created",
                IsSuccess = true

            };

        }

        public Task<List<User>> GetAllAdmins()
        {
            var admins = _db.Users.Where(a => a.Role == "Admin").ToListAsync();

            return admins;
        } 

         public Task<List<User>> GetAllMerchant()
        {
            var merchant = _db.Users.Where(a => a.Role == "Merchant").ToListAsync();

            return merchant;
        }

        public async Task<User> EditUserInfo(string userId, AdminRegisterViewModel usrUpdate)
        {
          
            var userToEdit = await _db.Users.FindAsync(userId);
            if(userToEdit != null)
            {
                userToEdit.UserName = usrUpdate.AdminName;
                userToEdit.Branch = usrUpdate.AdminBranch;
                userToEdit.Department = usrUpdate.AdminDepartment;
               await ResetPassword(userId, usrUpdate.AdminPassword);

                await _db.SaveChangesAsync();

                return userToEdit;
                

            }
            return null;
            
        } 

        //   public async Task<User> EditMerchnatInfo(string userId, MerchantRegisterViewModel usrUpdate)
        // {
          
        //     var userToEdit = await _db.Users.FindAsync(userId);
        //     if(userToEdit != null)
        //     {
        //         userToEdit.UserName = usrUpdate.Mer;
        //         userToEdit.Branch = usrUpdate.AdminBranch;
        //         userToEdit.Department = usrUpdate.AdminDepartment;
        //        await ResetPassword(userId, usrUpdate.AdminPassword);

        //         await _db.SaveChangesAsync();

        //         return userToEdit;
                

        //     }
        //     return null;
            
        // }

        public async Task ResetPassword(string userId, string newPasswd)
        {
            var user = await _userManage.FindByIdAsync(userId);

            var token = await _userManage.GeneratePasswordResetTokenAsync(user);

            var result = await _userManage.ResetPasswordAsync(user, token, newPasswd);

            

            



        }

        public async Task<string> DeleteUser(string userId)
        {
            var user = await _userManage.FindByIdAsync(userId);
             _db.Users.Remove(user);
            await _db.SaveChangesAsync();


            var result = "user deleted";

            return  result;

        }

        public async Task<UserMangageResponse> LoginAdminAsync(AdminLoginViewModel model)
        {
            var user = await _db.Users.SingleOrDefaultAsync(a => a.UserName == model.AdminName);

            if (user == null)
            {
                return new UserMangageResponse
                {
                    Message = "Incorect User Name",
                    IsSuccess = false,
                };
            }
            var result = await _userManage.CheckPasswordAsync(user, model.AdminPassword);


            if (!result)
            {
                return new UserLoginResponse
                {
                    Message = "Invalid Password",
                    IsSuccess = false
                };
            }


            var claims = new List<Claim> //[]
            {
                new Claim("AdminName", model.AdminName),
               // new Claim("FullName", model.),

                new Claim(ClaimTypes.NameIdentifier, user.Id),
               // new Claim(ClaimTypes.Role, userRoles)
                new Claim(ClaimTypes.Role, user.Role)
            };
            //roles 

            //   claims.Add(new Claim(ClaimTypes.Role, userRoles));



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));



            var token = new JwtSecurityToken(
                //issuer : _configuration["AuthSetings:Issuer"],
                //audience : _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserLoginResponse
            {
                // Id = user.Id,
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo,
                UserId = user.Id,
                FullName = user.UserName,
                //PhoneNumber = user.PhoneNumber,
                //AccountNumber = user.AccountNumber,
                //Email = user.Email,
                Role = user.Role,
                CreatedAt = DateTime.Now



            };


        }

        public async Task<UserMangageResponse> RegisterMerchantAsync(MerchantRegisterViewModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Register Model is Null");
            } 

            var merchantDate = _db.PersonalAccounts.FirstOrDefault(p => p.NumberPhone1Pa == model.MerchantPhoneNumber && p.AccountNamberPa == model.MerchantAccountNumber); 
            if (merchantDate == null)
            {
                return new UserMangageResponse
                {
                    Message = "Merchant Data Incorrect",
                    IsSuccess = false
                };
            } 

            bool IsPhoneNumberAlreadyRegistered = _db.Users.Any(u => u.PhoneNumber == model.MerchantPhoneNumber);
             if(IsPhoneNumberAlreadyRegistered)
            {
                return new UserMangageResponse
                {
                    Message = "PhoneNumber Already Exist", 
                    IsSuccess = false

                };
            }
            if (model.MerchantPassword != model.MerchantConfirmPassword) 
            {
                return new UserMangageResponse
                {
                    Message = "Wrong Register Inputs",
                    IsSuccess = false
                };
            }
            //if (IsPhoneAlreadyRegistered)
            //{
            //    return new UserMangeResponse
            //    {
            //        Message = "Phone number already exists",
            //        IsSuccess = false
            //    };
            //}

            var identityUser = new User
            {
                //Email = model.Email,
               // UserName = model.MerchantName,
              //  Branch = "Merchant",
              //  Department = "Merchant",
              //  PhoneNumber = model.MerchantPhoneNumber, 
               // AccountNumber = model.MerchantAccountNumber, 
               // IBAN = model.MerchantIBAN, 
               // BussinessName = model.BussinessName,

                //PhoneNumber = model.PhoneNumber,
                //AccountNumber = model.AccountNumber, 

              //  Role = UserRoles.Merchant,

               UserName = merchantDate.FullNameArabPa,
                
               // UserName = model.MerchantName,
                Branch = "Merchant",
                Department = "Merchant",
                PhoneNumber = merchantDate.NumberPhone1Pa,
               // PhoneNumber = model.MerchantPhoneNumber, 
              //  AccountNumber = model.MerchantAccountNumber, 
                AccountNumber = merchantDate.AccountNamberPa,
                IBAN = model.MerchantIBAN, 
                BussinessName = model.BussinessName,

                //PhoneNumber = model.PhoneNumber,
                //AccountNumber = model.AccountNumber,

                Role = UserRoles.Merchant,
            };
            var result = await _userManage.CreateAsync(identityUser, model.MerchantPassword);
            if (!result.Succeeded)
            {

                return new UserMangageResponse
                {
                    Message = "User did not created",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };


            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Merchant))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Merchant));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Merchant))
            {
                await _userManage.AddToRoleAsync(identityUser, UserRoles.Merchant);
            }


            return new UserMangageResponse
            {

                Message = "User Created",
                IsSuccess = true

            };

        }

        public async Task<UserLoginResponse> LoginMechantAsync(MerchantLoginViewModel model)
        {
            var user = await _db.Users.SingleOrDefaultAsync(m => m.PhoneNumber == model.MerchantPhoneNumber);

            if (user == null)
            {
                return new UserLoginResponse
                {
                    Message = "Incorect User Name",
                    IsSuccess = false,
                };
            }
            var result = await _userManage.CheckPasswordAsync(user, model.MerchantPassword);


            if (!result)
            {
                return new UserLoginResponse
                {
                    Message = "Invalid Password",
                    IsSuccess = false
                };
            }


            var claims = new List<Claim> //[]
            {
                new Claim("MerchantPhoneNumber", model.MerchantPhoneNumber),
               // new Claim("FullName", model.),

                new Claim(ClaimTypes.NameIdentifier, user.Id),
               // new Claim(ClaimTypes.Role, userRoles)
                new Claim(ClaimTypes.Role, user.Role)
            };
            //roles 

            //   claims.Add(new Claim(ClaimTypes.Role, userRoles));



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));



            var token = new JwtSecurityToken(
                //issuer : _configuration["AuthSetings:Issuer"],
                //audience : _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserLoginResponse
            {
                // Id = user.Id,
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo,
                UserId = user.Id,
                FullName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                AccountNumber = user.AccountNumber, 
                BussinessName = user.BussinessName,
                //Email = user.Email,
                IBAN = user.IBAN,
                Role = user.Role,
                CreatedAt = DateTime.Now



            };
        } 

           public async Task<AskBank> SendAskAccountStatus(AskBank askBank)
        {
            if (askBank.AccountNumberRequestInfo != null)
            {
               var isExist = await CheckAccountNumberExistsAndRetriveRelatedData(askBank.AccountNumberRequestInfo); 
              if(isExist !=null)
                {
                    AskBank ask = new AskBank
                    {
                        AccountNumberRequestInfo = askBank.AccountNumberRequestInfo,
                        RequestInfoDateTime =  DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")),
                        UserId = askBank.UserId, 
                        PhoneNumberRelatedToAccountNumber = isExist.PhoneNumberRelatedToAccountNumber, 
                        CustomerNumberRelatedToAccountNumber = isExist.CustomerNumberRelatedToAccountNumber, 
                        RequestAnswer = "انتظار", 
                        Notes = "لا توجد بعد"

                    }; 
                   await _db.AskBanks.AddAsync(ask);
                   await _db.SaveChangesAsync();

                    return ask;
                }
                return null;
              



            }
            return null;

        }

        public async Task<AskBank> CheckAccountNumberExistsAndRetriveRelatedData(string acctNum)
        {
            var acctExist = await _db.PersonalAccounts.FirstOrDefaultAsync(a => a.AccountNamberPa == acctNum); 
            if(acctExist != null)
            {
                AskBank askBankRetriveData = new AskBank{ 
                    AccountNumberRequestInfo = acctExist.AccountNamberPa, 
                    PhoneNumberRelatedToAccountNumber = acctExist.NumberPhone1Pa, 
                    CustomerNumberRelatedToAccountNumber = acctExist.FullNameArabPa

                } ; 

                return askBankRetriveData;
            }
            else
            {
                return null;
            } 
            
        }

        public async Task<UserMangageResponse> RegisterBranchEmployeeRespondent(BranchEmployeeRespondentViewModel model)
        {

            
            if (model == null)
            {
                throw new NullReferenceException("Register Model is Null");
            }
            if (model.BranchEmployeeRespondentPassword != model.BranchEmployeeRespondentConfirmPassword )
            {
                return new UserMangageResponse
                {
                    Message = "Wrong Register Inputs",
                    IsSuccess = false
                };
            }
            //if (IsPhoneAlreadyRegistered)
            //{
            //    return new UserMangeResponse
            //    {
            //        Message = "Phone number already exists",
            //        IsSuccess = false
            //    };
            //}

            var identityUser = new User
            {
                //Email = model.Email,
                UserName = model.BranchEmployeeRespondentName,
                Branch = model.BranchEmployeeRespondentBranch, 
                Department = model.BranchEmployeeRespondentDepartment,
           
                //PhoneNumber = model.PhoneNumber,
                //AccountNumber = model.AccountNumber,

                Role = UserRoles.BranchEmployeeRespondent,
            };
            var result = await _userManage.CreateAsync(identityUser, model.BranchEmployeeRespondentPassword);
            if (!result.Succeeded)
            {

                return new UserMangageResponse
                {
                    Message = "User did not created",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };


            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Merchant))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Merchant));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            } 
             if (!await _roleManager.RoleExistsAsync(UserRoles.BranchEmployeeRespondent))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.BranchEmployeeRespondent));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManage.AddToRoleAsync(identityUser, UserRoles.BranchEmployeeRespondent);
            }


            return new UserMangageResponse
            {

                Message = "User Created",
                IsSuccess = true

            };

            
        }

        public async Task<UserLoginResponse> LoginBranchEmployeeRespondent(BranchEmployeeRespondentLoginViewModel model)
        {
            var user = await _db.Users.SingleOrDefaultAsync(a => a.UserName == model.BranchEmployeeRespondentName);

            if (user == null)
            {
                return new UserLoginResponse
                {
                    Message = "Incorect User Name",
                    IsSuccess = false,
                };
            }
            var result = await _userManage.CheckPasswordAsync(user, model.BranchEmployeeRespondentPassword);


            if (!result)
            {
                return new UserLoginResponse
                {
                    Message = "Invalid Password",
                    IsSuccess = false
                };
            }


            var claims = new List<Claim> //[]
            {
                new Claim("BranchEmployeeRespondentName", model.BranchEmployeeRespondentName),
               // new Claim("FullName", model.),

                new Claim(ClaimTypes.NameIdentifier, user.Id),
               // new Claim(ClaimTypes.Role, userRoles)
                new Claim(ClaimTypes.Role, user.Role)
            };
            //roles 

            //   claims.Add(new Claim(ClaimTypes.Role, userRoles));



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));



            var token = new JwtSecurityToken(
                //issuer : _configuration["AuthSetings:Issuer"],
                //audience : _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserLoginResponse
            {
                // Id = user.Id,
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo,
                UserId = user.Id,
                FullName = user.UserName,
                //PhoneNumber = user.PhoneNumber,
                //AccountNumber = user.AccountNumber,
                //Email = user.Email,
                Role = user.Role,
                CreatedAt = DateTime.Now



            };
        } 

        public   IList<AskBank> GetAllRequestInfo () 
        {  
            IList<AskBank> requests =  _db.AskBanks.ToList();
            return requests;

        }  

        public async Task<AskBank> SendAskBankAnswer (long askBankId, AskBank askBank) 
        {
            var askForm = await _db.AskBanks.FindAsync(askBankId); 

            if (askForm != null)  
            { 
                askForm.RequestAnswer = askBank.RequestAnswer;
                askForm.Notes = askBank.Notes;
                askForm.RequestAnswerDateTime = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")); 

                await _db.SaveChangesAsync();

                return askForm;


            } 

            return null;
            
        } 

         public IList<AskBank> GetAskBanksByUserId(string userId)
        {
            var askBankByUser = _db.AskBanks.Where(b => b.UserId == userId).ToList();

            return askBankByUser;
        } 

          public  PersonalAccount CheckIfCustomerIsAllowToTakeMurabaha(string acctNum)
        {
            if(acctNum != null)
            {
                var dataresult = _db.AskBanks.FirstOrDefault(ask => ask.AccountNumberRequestInfo == acctNum);
                if(dataresult != null)
                {
                    if(dataresult.RequestAnswer == "منح")
                    {
                        var resultToRetrive = _db.PersonalAccounts.FirstOrDefault(p => p.AccountNamberPa == dataresult.AccountNumberRequestInfo);
                        return resultToRetrive;
                    }
                }
                return null;
            }
            return null;
        }  
        

         public  PersonalAccount CheckIfCustomerIsAllowToTakeMurabaha2 (string acctNum)
        {
            if(acctNum != null)
            {
                var dataresult = _db.PersonalAccounts.FirstOrDefault(p => p.AccountNamberPa== acctNum);
                if(dataresult != null)
                { 
                    if(dataresult.AllowedToTakeTmweel != null) {
                    if(dataresult.AllowedToTakeTmweel.Replace(" ", String.Empty) == "يمنح")
                    {
                        var resultToRetrive = _db.PersonalAccounts.FirstOrDefault(p => p.AccountNamberPa == dataresult.AccountNamberPa);
                        return resultToRetrive;
                    } 
                    }
                 
                }
                return null;
            }
            return null;
        }  

        public Test GetPayInfo(InputTest npt)
        {
             double[] pcir = {0, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08, 0.08 };
             Test test = new Test(); 

               if (npt.BillAmt * (1 + 0.08) < (npt.MPay * 0.4 * 36) || (1 + 0.08) == npt.MPay * 0.4 * 36)
            {
                //  IR = IRRelatedToPayCount(payCount);  
                for (int i = 0; i < pcir.Length + 1; i++)
                {
                    // Console.WriteLine(  "value {0}, and index of {1}",pcir[i], i+1); 
                    if (npt.BillAmt * (1 + pcir[i]) < (npt.MPay * 0.4 * i + 1))
                    {

                        Console.WriteLine("IR = {0} ,  PC = {1}", pcir[i], i);

                        test = new Test { PayCount = i, PayAmt = npt.MPay * 0.4 - (((npt.MPay * 0.4 * i) - (npt.BillAmt * (1 + pcir[i]))) / i), Start =  DateTime.Parse(DateTime.Now.Date.ToShortDateString()), End = DateTime.Parse(DateTime.Now.AddMonths(i).Date.ToShortDateString()), 
                                         ThreePercentageFromBillAmt = npt.BillAmt * 0.03  ,  
                                       //  ThreePercentgeFromPayAmt =  (npt.MPay * 0.4 - (((npt.MPay * 0.4 * i) - (npt.BillAmt * (1 + pcir[i]))) / i) ) * 0.03, 
                                         MerchanMontlyReturnPay = npt.BillAmt / i, 
                                        // EightPercentageOfPayAmt = npt.BillAmt * 0.08 / i,
                                         ThreePercentageFromEightPerectage = (npt.BillAmt * 0.08 /i ) * 0.03, 
                                         MonthlyInsurancePay = (npt.BillAmt * 0.03) / i , 
                                         NetBankGainMontly = (npt.BillAmt * 0.08 / i ) - ((npt.BillAmt * 0.03) / i)
                                         };
                        // Console.WriteLine(npt.MPay * 0.4 - (((npt.MPay * 0.4 * i) - (npt.BillAmt * (1 + pcir[i]))) / i));
                        // Console.WriteLine(npt.MPay);
                        //Console.WriteLine(npt.BillAmt);
                        //Console.WriteLine(npt.MPay * 0.4);
                      //  Console.WriteLine((((npt.MPay * 0.4 * i) - (npt.BillAmt * (1 + pcir[i]))) / i));
                        break;








                    }
                }

                return test;


            }
            return test ;
           
        

        } 

          public Test GetPayInfo2(InputTest npt)
        {
             double[] pcir = {0, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05 };
             Test test = new Test(); 

               if (npt.BillAmt * (1 + 0.05) < (npt.MPay * 0.35 * 36) || (1 + 0.05) == npt.MPay * 0.35 * 36)
            {
                //  IR = IRRelatedToPayCount(payCount);  
                for (int i = 0; i < pcir.Length + 1; i++)
                {
                    // Console.WriteLine(  "value {0}, and index of {1}",pcir[i], i+1); 
                    if (npt.BillAmt * (1 + pcir[i]) < (npt.MPay * 0.35 * i + 1))
                    {

                        Console.WriteLine("IR = {0} ,  PC = {1}", pcir[i], i);

                        test = new Test { PayCount = i+1, PayAmt = npt.MPay * 0.35 - (((npt.MPay * 0.35 * i) - (npt.BillAmt * (1 + pcir[i]))) / i), Start =  DateTime.Parse(DateTime.Now.Date.ToShortDateString()), End = DateTime.Parse(DateTime.Now.AddMonths(i).Date.ToShortDateString()), 
                                         ThreePercentageFromBillAmt = npt.BillAmt * 0.03  ,  
                                       //  ThreePercentgeFromPayAmt =  (npt.MPay * 0.35 - (((npt.MPay * 0.35 * i) - (npt.BillAmt * (1 + pcir[i]))) / i) ) * 0.03, 
                                         MerchanMontlyReturnPay = npt.BillAmt / i, 
                                       //  EightPercentageOfPayAmt = npt.BillAmt * 0.05 / i,
                                         ThreePercentageFromEightPerectage = (npt.BillAmt * 0.05 /i ) * 0.03, 
                                         MonthlyInsurancePay = (npt.BillAmt * 0.03) / i , 
                                         NetBankGainMontly = (npt.BillAmt * 0.05 / i ) - ((npt.BillAmt * 0.03) / i)
                                         };
                        // Console.WriteLine(npt.MPay * 0.35 - (((npt.MPay * 0.4 * i) - (npt.BillAmt * (1 + pcir[i]))) / i));
                        // Console.WriteLine(npt.MPay);
                        //Console.WriteLine(npt.BillAmt);
                        //Console.WriteLine(npt.MPay * 0.4);
                      //  Console.WriteLine((((npt.MPay * 0.4 * i) - (npt.BillAmt * (1 + pcir[i]))) / i));
                        break;








                    }
                }

                return test;


            }
            return test ;
           
        

        } 

      public OutputValues GetPayInfo3(InputValues inputValues)
        { 
            //check tmweel flag  
            PersonalAccount paCheck = _db.PersonalAccounts.FirstOrDefault(p => p.AccountNamberPa == inputValues.CustomerAcctNum);
            bool checkIsOnTmweel = paCheck.IsOnTmweelFlag; 
            if(checkIsOnTmweel == false){
            OutputValues outputValues = new OutputValues(); 
            
            if(inputValues.BillAmount <= inputValues.MonthlySalary * 0.35 * 18)
            {
                if(inputValues.NumberOfMonths < 1 || inputValues.NumberOfMonths > 18)
                {
                    return new OutputValues
                    {
                        IsError = true,
                        ErrorMessege = "Number of Months bigger than 18 or smaller than 1",
                    };

                  

 
                }
                outputValues = new OutputValues
                {
                    PayCount = inputValues.NumberOfMonths, 
                    // MerchantPayMonthly = inputValues.BillAmount / inputValues.NumberOfMonths,  
                     MerchantPayMonthly = inputValues.BillAmount / (inputValues.NumberOfMonths * 1.1),  
                     MonthlyPay = inputValues.BillAmount / inputValues.NumberOfMonths, 
                    
                    InsuranceAmount = inputValues.BillAmount * 0.03, 
                   // BankRevenueMonthly = inputValues.BillAmount * 0.05 / inputValues.NumberOfMonths,  
                    BankRevenueMonthly = (inputValues.BillAmount / inputValues.NumberOfMonths) - inputValues.BillAmount / (inputValues.NumberOfMonths * 1.1) ,
                   
                   // MonthlyPay = (inputValues.BillAmount / inputValues.NumberOfMonths )+( inputValues.BillAmount * 0.05 / inputValues.NumberOfMonths),
                    StartDate = DateTime.Now.AddMonths(1), 
                    EndDate = DateTime.Now.AddMonths(inputValues.NumberOfMonths),
                    IsError = false,
                    ErrorMessege = "No Errors",

                };

                if(outputValues.MerchantPayMonthly <= inputValues.MonthlySalary * 0.35)
                {
                    
                    return outputValues;
                }
                else
                {
                    return new OutputValues
                       {
                           IsError = true,
                           ErrorMessege = "Ratio Of Salary Above the limit"
                      };

                }
              

            } 
            return new OutputValues 
            { 
                IsError = true, 
                ErrorMessege = "This Customer Already on tmweel"
                

            };
            }
            return new OutputValues
            {
                IsError = true,
                ErrorMessege = "Bill Amount Bigger than allowed "
            };


        }

        public OutputValues GetPayInfo4(InputValues inputValues)
        {
             OutputValues outputValues = new OutputValues(); 

            if(inputValues.BillAmount <= inputValues.MonthlySalary * 0.35 * 36 )
            {
                if (inputValues.NumberOfMonths >=1 && inputValues.NumberOfMonths <= 12)
                {

                    outputValues = new OutputValues
                    {
                        PayCount = inputValues.NumberOfMonths,
                        InsuranceAmount = inputValues.BillAmount * 0.03,
                        MonthlyPay = (inputValues.BillAmount / inputValues.NumberOfMonths) + (inputValues.BillAmount * 0.05) / inputValues.NumberOfMonths,
                        MerchantPayMonthly = inputValues.BillAmount /  inputValues.NumberOfMonths,
                        BankRevenueMonthly = (inputValues.BillAmount * 0.05) / inputValues.NumberOfMonths,

                        StartDate = DateTime.Now.AddMonths(1),
                        EndDate = DateTime.Now.AddMonths(inputValues.NumberOfMonths),


                    };
                  
                    return outputValues;
                }


                if (inputValues.NumberOfMonths > 12 && inputValues.NumberOfMonths <= 24)
                {
                    outputValues = new OutputValues
                    {
                        PayCount = inputValues.NumberOfMonths,
                        InsuranceAmount = inputValues.BillAmount * 0.03,
                        MonthlyPay = (inputValues.BillAmount / inputValues.NumberOfMonths) + (inputValues.BillAmount * 0.075) / inputValues.NumberOfMonths,
                        MerchantPayMonthly = inputValues.BillAmount / inputValues.NumberOfMonths,
                        BankRevenueMonthly = (inputValues.BillAmount * 0.075) / inputValues.NumberOfMonths,

                        StartDate = DateTime.Now.AddMonths(1),
                        EndDate = DateTime.Now.AddMonths(inputValues.NumberOfMonths),


                    };
                
                    return outputValues;
                }

                if (inputValues.NumberOfMonths > 24 && inputValues.NumberOfMonths <= 36)
                {
                    outputValues = new OutputValues
                    {
                        PayCount = inputValues.NumberOfMonths,
                        InsuranceAmount = inputValues.BillAmount * 0.03,
                        MonthlyPay = (inputValues.BillAmount / inputValues.NumberOfMonths) + (inputValues.BillAmount * 0.1) / inputValues.NumberOfMonths,
                        MerchantPayMonthly = inputValues.BillAmount / inputValues.NumberOfMonths,
                        BankRevenueMonthly = (inputValues.BillAmount * 0.1) / inputValues.NumberOfMonths,

                        StartDate = DateTime.Now.AddMonths(1),
                        EndDate = DateTime.Now.AddMonths(inputValues.NumberOfMonths),


                    };
                 
                    return outputValues;
                }

                return outputValues;
            }

            return outputValues;

        }



        
       public async  Task<UserMangageResponse> RegisterInsuranceCompany (InsuranceCompany model) 
       {
             if (model == null)
            {
                throw new NullReferenceException("Register Model is Null");
            }
            if (model.Password != model.ConfirmPassword )
            {
                return new UserMangageResponse
                {
                    Message = "Wrong Register Inputs",
                    IsSuccess = false
                };
            }
            //if (IsPhoneAlreadyRegistered)
            //{
            //    return new UserMangeResponse
            //    {
            //        Message = "Phone number already exists",
            //        IsSuccess = false
            //    };
            //}

            var identityUser = new User
            {
                //Email = model.Email,
                UserName = model.Name,
                Branch = "InsuranceCompany",
                Department = "InsuranceCompany", 
                PhoneNumber = model.PhoneNumber, 

           
                //PhoneNumber = model.PhoneNumber,
                //AccountNumber = model.AccountNumber,

                Role = UserRoles.InsuranceCompany,
            };
            var result = await _userManage.CreateAsync(identityUser, model.Password);
            if (!result.Succeeded)
            {

                return new UserMangageResponse
                {
                    Message = "User did not created",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };


            }

            // if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            // {
            //     await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            // }
            // if (!await _roleManager.RoleExistsAsync(UserRoles.Merchant))
            // {
            //     await _roleManager.CreateAsync(new IdentityRole(UserRoles.Merchant));
            // }

            // if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
            // {
            //     await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
            // }
            // if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            // {
            //     await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            // } 
            //  if (!await _roleManager.RoleExistsAsync(UserRoles.BranchEmployeeRespondent))
            // {
            //     await _roleManager.CreateAsync(new IdentityRole(UserRoles.BranchEmployeeRespondent));
            // }

            // if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            // {
            //     await _userManage.AddToRoleAsync(identityUser, UserRoles.BranchEmployeeRespondent);
            // }


            return new UserMangageResponse
            {

                Message = "User Created",
                IsSuccess = true

            };

       } 

       public async Task<UserLoginResponse> LoginInsuracneCompany (InsuranceCompanyLogin model) 
       {
            var user = await _db.Users.SingleOrDefaultAsync(a => a.PhoneNumber == model.PhoneNumber);

            if (user == null)
            {
                return new UserLoginResponse
                {
                    Message = "Incorect User Name",
                    IsSuccess = false,
                };
            }
            var result = await _userManage.CheckPasswordAsync(user, model.Password);


            if (!result)
            {
                return new UserLoginResponse
                {
                    Message = "Invalid Password",
                    IsSuccess = false
                };
            }


            var claims = new List<Claim> //[]
            {
                new Claim("Name", model.PhoneNumber),
               // new Claim("FullName", model.),

                new Claim(ClaimTypes.NameIdentifier, user.Id),
               // new Claim(ClaimTypes.Role, userRoles)
                new Claim(ClaimTypes.Role, user.Role)
            };
            //roles 

            //   claims.Add(new Claim(ClaimTypes.Role, userRoles));



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));



            var token = new JwtSecurityToken(
                //issuer : _configuration["AuthSetings:Issuer"],
                //audience : _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserLoginResponse
            {
                // Id = user.Id,
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo,
                UserId = user.Id,
                FullName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                //AccountNumber = user.AccountNumber,
                //Email = user.Email,
                Role = user.Role,
                CreatedAt = DateTime.Now



            };
       } 

        public async Task<UserMangageResponse> RegisterOperationsEmployee (BranchEmployeeRespondentViewModel model) 
        { 
              
            if (model == null)
            {
                throw new NullReferenceException("Register Model is Null");
            }
            if (model.BranchEmployeeRespondentPassword != model.BranchEmployeeRespondentConfirmPassword )
            {
                return new UserMangageResponse
                {
                    Message = "Wrong Register Inputs",
                    IsSuccess = false
                };
            }
            //if (IsPhoneAlreadyRegistered)
            //{
            //    return new UserMangeResponse
            //    {
            //        Message = "Phone number already exists",
            //        IsSuccess = false
            //    };
            //}

            var identityUser = new User
            {
                //Email = model.Email,
                UserName = model.BranchEmployeeRespondentName,
                Branch = model.BranchEmployeeRespondentBranch, 
                Department = model.BranchEmployeeRespondentDepartment,
           
                //PhoneNumber = model.PhoneNumber,
                //AccountNumber = model.AccountNumber,

                Role = UserRoles.OperationsEmployee,
            };
            var result = await _userManage.CreateAsync(identityUser, model.BranchEmployeeRespondentPassword);
            if (!result.Succeeded)
            {

                return new UserMangageResponse
                {
                    Message = "User did not created",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };


            }

            // if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            // {
            //     await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            // }
            // if (!await _roleManager.RoleExistsAsync(UserRoles.Merchant))
            // {
            //     await _roleManager.CreateAsync(new IdentityRole(UserRoles.Merchant));
            // }

            // if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
            // {
            //     await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
            // }
            // if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            // {
            //     await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            // } 
            //  if (!await _roleManager.RoleExistsAsync(UserRoles.BranchEmployeeRespondent))
            // {
            //     await _roleManager.CreateAsync(new IdentityRole(UserRoles.BranchEmployeeRespondent));
            // }

            // if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            // {
            //     await _userManage.AddToRoleAsync(identityUser, UserRoles.BranchEmployeeRespondent);
            // }


            return new UserMangageResponse
            {

                Message = "User Created",
                IsSuccess = true

            };


        } 

          public async Task<UserLoginResponse> LoginOperationsEmployee(BranchEmployeeRespondentLoginViewModel model)
        {
            var user = await _db.Users.SingleOrDefaultAsync(a => a.UserName == model.BranchEmployeeRespondentName);

            if (user == null)
            {
                return new UserLoginResponse
                {
                    Message = "Incorect User Name",
                    IsSuccess = false,
                };
            }
            var result = await _userManage.CheckPasswordAsync(user, model.BranchEmployeeRespondentPassword);


            if (!result)
            {
                return new UserLoginResponse
                {
                    Message = "Invalid Password",
                    IsSuccess = false
                };
            }


            var claims = new List<Claim> //[]
            {
                new Claim("OperationEmployee", model.BranchEmployeeRespondentName),
               // new Claim("FullName", model.),

                new Claim(ClaimTypes.NameIdentifier, user.Id),
               // new Claim(ClaimTypes.Role, userRoles)
                new Claim(ClaimTypes.Role, user.Role)
            };
            //roles 

            //   claims.Add(new Claim(ClaimTypes.Role, userRoles));



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));



            var token = new JwtSecurityToken(
                //issuer : _configuration["AuthSetings:Issuer"],
                //audience : _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserLoginResponse
            {
                // Id = user.Id,
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo,
                UserId = user.Id,
                FullName = user.UserName,
                //PhoneNumber = user.PhoneNumber,
                //AccountNumber = user.AccountNumber,
                //Email = user.Email,
                Role = user.Role,
                CreatedAt = DateTime.Now



            };
        }  

         public Bill SendBillDataToServer(Bill bill) 
         { 

             if(bill != null ) 
             {
             Bill billToSend = new Bill {  
                 
                 BillDate = bill.BillDate,
                 BillSeq = bill.BillSeq,
                 MerchantNameForBill = bill.MerchantNameForBill, 
                 MerchantAcctNumForBill = bill.MerchantAcctNumForBill, 
                 MerchantPhoneNumber = bill.MerchantPhoneNumber, 
                 MerchantBussinessName = bill.MerchantBussinessName, 
               //  Goods = bill.Goods, 
                 BillAmount = bill.BillAmount, 
                //  BillDiscount = bill.BillDiscount, 
                 CustomerNameForBill = bill.CustomerAcctNumForBill, 
                 CustomerAcctNumForBill = bill.CustomerAcctNumForBill, 
                 CustomerBirth = bill.CustomerBirth,
                 CustomerAddressForBill = bill.CustomerAddressForBill, 
                 CustomerPhoneNumberForBill = bill.CustomerPhoneNumberForBill, 
                 TotalSalaryForBill = bill.TotalSalaryForBill, 
                 PayCountForBill = bill.PayCountForBill, 
                 MonthlyPayAmt = bill.MonthlyPayAmt, 
                 StartPayDateForBill = bill.StartPayDateForBill, 
                 EndPayDateForBill = bill.EndPayDateForBill, 
                 BillDoc1 = bill.BillDoc1, 
                 Bill1IncertedOn = DateTime.Now , 
                 UserId = bill.UserId

             }; 
                 return billToSend;

             } 

             return null;

             
         } 



           public bool AccountNumberOperations(string accNum)
        {
          //  Random generator = new Random();
        //   string randomNumberForSms = generator.Next(0, 1000000).ToString("D6");
           string randomNumberForSms = "123456"; 

            if (accNum != null)
            {
                Console.WriteLine("aaa");
                var account = _db.PersonalAccounts.FirstOrDefault(p => p.NumberPhone1Pa == accNum.ToString());
               
               // var account = _db.PersonalAccounts.FirstOrDefault(p => p.NumberPhone1Pa == accNum); 
               // Console.WriteLine(account);
                if (account != null)
                { 

                    Console.WriteLine("bbb");
                    SmsVerificationCode code = new SmsVerificationCode
                    {
                        AccountNumber = account.AccountNamberPa,
                        PersonalAccountIdPa = account.IdPa,
                        PhoneNumber = account.NumberPhone1Pa,
                        VerificationCode = randomNumberForSms,
                        InsetedOn = DateTime.Now,
                        // IsExpired = false,
                        VerivicationCodeExpireOn = DateTime.Now.AddMinutes(2),
                    };
                    _db.SmsVerificationCodes.Add(code);
                    _db.SaveChanges();


                    //  const string BaseUrl = "https://oursms.app/api/v1/SMS/Add/SendOneSms";


                    // string r = "12345";
                   // string phoneNumber = "964" + account.NumberPhone1Pa.Remove(0,1).ToString();

                   // HttpClientHandler clientHandler = new HttpClientHandler();
                  //  clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                    // Pass the handler to httpclient(from you are calling api)
                   // HttpClient client = new HttpClient(clientHandler);
                    //  HttpClient client = new HttpClient();
                   // client.DefaultRequestHeaders.Accept.Clear();
                   // client.DefaultRequestHeaders.Accept.Add(
                   //   new MediaTypeWithQualityHeaderValue("application/json"));
                    //  client.DefaultRequestHeaders.Add("Content-Type", "application/json");

                   // var stringTask = client.GetStringAsync($"https://151.236.190.243:888/send-sms?token=KIRYN5NI0pgh2hm1m3AdMgc3TfWmbL&lang=ar&receiver={phoneNumber}&vars={randomNumberForSms}");

                 //   var msg = await stringTask;
                 //  Console.Write(stringTask);

                 //   return (msg.ToString());

                    //IRestClient _client;

                    //_client = new RestClient(BaseUrl);
                    //_client.Timeout = -1;

                    //JObject jObjectbody = new JObject();
                    //jObjectbody.Add("UserId", 221);
                    //jObjectbody.Add("key", "FB4548C13A9471195FA4125636A34509");
                    //jObjectbody.Add("phoneNumber", "964" + code.PhoneNumber.Remove(0, 1).ToString());
                    //jObjectbody.Add("Message", code.VerificationCode);
                    //RestRequest request = new RestRequest(Method.POST);
                    //request.AddHeader("Content-Type", "application/json");
                    //request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

                    //IRestResponse response = _client.Execute(request);
                  //  Console.WriteLine("bbbddd");
                    return true;

                } 
             //   Console.WriteLine("GGGGG");
                return false;

            } 
          //  Console.WriteLine("FFFF");

            return false;
        }

        public PersonalAccount VerificationAndPhoneNumberOperations(string code)
        {
            if (code != null)
           {
                var verificationCopmapre = _db.SmsVerificationCodes.FirstOrDefault(v => v.VerificationCode == code 
                && v.VerivicationCodeExpireOn > DateTime.Now);
               //&& 
             //   v.VerivicationCodeExpireOn <= DateTime.Now.AddMinutes(2));
               if(verificationCopmapre != null)
               {
                    var personalAccountData = _db.PersonalAccounts.FirstOrDefault(p => p.AccountNamberPa == verificationCopmapre.AccountNumber);
                   _db.SmsVerificationCodes.Remove(verificationCopmapre);
                   _db.SaveChanges();
                   return personalAccountData;

                        }
                       return null;
                    }
                   return null;

        }

        public Good AddGood(Good good)
        {
            if (good == null)
            {
                throw new NullReferenceException("Invalid Good");
            } 

            var goodToAdd = new Good {
               // GoodSeq = good.GoodSeq,  
             //   BillSeq = good.BillSeq,
               // BillId = good.BillId,
                GoodName = good.GoodName, 
                GoodStore = good.GoodStore, 
                GoodDimensions = good.GoodDimensions, 
                GoodQuentity = good.GoodQuentity, 
                // SumPrice = good.SumPrice,  
                Currency = good.Currency,
                SumPrice = Math.Round(good.UnitPrice * good.GoodQuentity * 1.1,3),
                UnitPrice = Math.Round(good.UnitPrice * 1.1),
                CustomerAcctNum = good.CustomerAcctNum, 
                MerchantAcctNum = good.MerchantAcctNum,


                BillId = good.BillId, 


            }; 

            _db.Goods.Add(goodToAdd); 
            _db.SaveChanges(); 
            return goodToAdd;
        }

        public Good EditGood(string goodId, Good good)
        {
            var goodToEdit = _db.Goods.Find(goodId);
            if(goodToEdit != null)
            {
                goodToEdit.GoodName = good.GoodName;
                goodToEdit.GoodStore = good.GoodStore;
                goodToEdit.GoodDimensions = good.GoodDimensions;
                goodToEdit.GoodQuentity = good.GoodQuentity; 
                goodToEdit.Currency = good.Currency;
              //  goodToEdit.SumPrice = Math.Round(good.UnitPrice * good.GoodQuentity * 1.1,3);
                goodToEdit.SumPrice = Math.Round(good.UnitPrice * good.GoodQuentity,3);
              //  goodToEdit.SumPrice = good.SumPrice;
               // goodToEdit.UnitPrice = Math.Round(good.UnitPrice * 1.1); 
                goodToEdit.UnitPrice = Math.Round(good.UnitPrice);
            

               // _db.SaveChangesAsync(); 
               _db.SaveChanges();

                return goodToEdit;
                

            }
            return null;
        } 


          public Good MainGoodEdit(string goodId, Good good)
        {
            var goodToEdit = _db.Goods.Find(goodId);
            if(goodToEdit != null)
            {
                goodToEdit.GoodName = good.GoodName;
                goodToEdit.GoodStore = good.GoodStore;
                goodToEdit.GoodDimensions = good.GoodDimensions;
                goodToEdit.GoodQuentity = good.GoodQuentity; 
                goodToEdit.Currency = good.Currency;
              //  goodToEdit.SumPrice = Math.Round(good.UnitPrice * good.GoodQuentity * 1.1,3);
                goodToEdit.SumPrice = Math.Round(good.UnitPrice * good.GoodQuentity * 1.1,3);
              //  goodToEdit.SumPrice = good.SumPrice;
               // goodToEdit.UnitPrice = Math.Round(good.UnitPrice * 1.1); 
                goodToEdit.UnitPrice = Math.Round(good.UnitPrice * 1.1);
            

               // _db.SaveChangesAsync(); 
               _db.SaveChanges();

                return goodToEdit;
                

            }
            return null;
        }

        public string DeleteGood(string goodId)
        {
             var goodToDelete = _db.Goods.Find(goodId);
             _db.Goods.Remove(goodToDelete);
             _db.SaveChanges();


            var result = "user deleted";

            return result;
        }

        public List<Good> GetAllGoods(string billId)
        {
          // List<Good> goods = _db.Goods.Where(g => g.CustomerAcctNum == goodsGet.CustomerAcctNum && g.MerchantAcctNum == goodsGet.MerchantAcctNum).ToList(); 
            List<Good> goods = _db.Goods.Where(g => g.BillId == billId).ToList();
           Console.WriteLine(goods);
         //  List<Good> goods = _db.Goods.ToList();   
           

            return goods;
            
        }  

        public double CaltotalSum(string billId)  
        {
            double totalSum = 0;
           // var goodToSum = _db.Goods.Where(g =>  == billId).ToList();
           var goodToSum = _db.Goods.Where(g => g.BillId == billId).ToList();
            for(int i = 0 ; i<goodToSum.Count ; i++)   { 
                totalSum = totalSum + goodToSum[i].SumPrice;

            } 

            return totalSum;
            
        } 

       // public List<Bill> GetAllBillsBy(string  CustomerName, DateTime StartDate, DateTime EndDate , string MerchantName , string CompanyName) 
        public List<Bill> GetAllBillsBy(BillsSearch billsSearch) 
        {    




             if(billsSearch.RoleForSearch == "Operations") { 
              List<Bill> billOp ;   


              if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null && billsSearch.last24Hours == "true") 
                { 
                    billOp = _db.Bills.Where(b => b.BillDate >= DateTime.Now.AddHours(-24) && b.BillDate <= DateTime.Now.AddHours(24) 
                                              && b.FirstAuditFromCompanyEmployee == true && b.SecondAuditFromCompanyEmployee == true 
                                              && b.IsInsured == true
                                               //&& b.DirectiveManagerApproval == true
                                               ).ToList(); 
                    Console.WriteLine("jjjj");
                    return billOp; 

                }



            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("AllC1");
                billOp = _db.Bills.Where(b => b.FirstAuditFromCompanyEmployee == true && b.IsInsured == true 
                && b.FirstAuditFromCompanyEmployee == true && b.SecondAuditFromCompanyEmployee == true 
                                              && b.IsInsured == true 
                                              && b.DirectiveManagerApproval == true).OrderByDescending(b => b.BillDate).ToList();  
             //   Console.WriteLine(billsMD);
                return billOp; 
                

            }   


            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName != null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Merchant");
             
               billOp = _db.Bills.Where(b => b.MerchantNameForBill.Contains(billsSearch.MerchantName) &&  b.FirstAuditFromCompanyEmployee == true && b.SecondAuditFromCompanyEmployee == true 
                                              && b.IsInsured == true 
                                              && b.DirectiveManagerApproval == true).OrderByDescending(b => b.BillDate).ToList();

        
                return billOp; 
                

            }  

            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName != null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Customer");
             
               billOp = _db.Bills.Where(b => b.CustomerNameForBill.Contains(billsSearch.CustomerName) && b.FirstAuditFromCompanyEmployee == true && b.SecondAuditFromCompanyEmployee == true 
                                              && b.IsInsured == true 
                                              && b.DirectiveManagerApproval == true).OrderByDescending(b => b.BillDate).ToList();

           
                return billOp; 
                

            }  


            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName != null ) 
            {  
                Console.WriteLine("Company");
             
               billOp = _db.Bills.Where(b => b.MerchantBussinessName.Contains(billsSearch.CompanyName) && b.FirstAuditFromCompanyEmployee == true && b.SecondAuditFromCompanyEmployee == true 
                                              && b.IsInsured == true 
                                              && b.DirectiveManagerApproval == true).OrderByDescending(b => b.BillDate).ToList();

              
                return billOp; 
                

            }  


            if(billsSearch.EndDate.ToString() != "none"  && billsSearch.StartDate.ToString() != "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Date");
               
              billOp = _db.Bills.Where(b => b.BillDate > Convert.ToDateTime(billsSearch.StartDate)  && b.BillDate < Convert.ToDateTime(billsSearch.EndDate) && b.FirstAuditFromCompanyEmployee == true && b.SecondAuditFromCompanyEmployee == true 
                                              && b.IsInsured == true 
                                              && b.DirectiveManagerApproval == true).OrderByDescending(b => b.BillDate).ToList();

              
                return billOp; 
                

            } 

            

            else {
                Console.WriteLine("null");
                return null;
                
            } 

            }



            if(billsSearch.RoleForSearch == "ManagerDirecter") { 
              List<Bill> billsMD ;   


              if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null && billsSearch.last24Hours == "true") 
                { 
                    billsMD = _db.Bills.Where(b => b.BillDate >= DateTime.Now.AddHours(-24) && b.BillDate <= DateTime.Now.AddHours(24) 
                                              && b.FirstAuditFromCompanyEmployee == true && b.SecondAuditFromCompanyEmployee == true 
                                              && b.IsInsured == true).ToList(); 
                    Console.WriteLine("jjjj");
                    return billsMD; 

                }



            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("AllC1");
                billsMD = _db.Bills.Where(b => b.FirstAuditFromCompanyEmployee == true && b.IsInsured == true 
                && b.FirstAuditFromCompanyEmployee == true && b.SecondAuditFromCompanyEmployee == true 
                                              && b.IsInsured == true).OrderByDescending(b => b.BillDate).ToList();  
             //   Console.WriteLine(billsMD);
                return billsMD; 
                

            }   


            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName != null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Merchant");
             
               billsMD = _db.Bills.Where(b => b.MerchantNameForBill.Contains(billsSearch.MerchantName) &&  b.FirstAuditFromCompanyEmployee == true && b.SecondAuditFromCompanyEmployee == true 
                                              && b.IsInsured == true).OrderByDescending(b => b.BillDate).ToList();

        
                return billsMD; 
                

            }  

            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName != null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Customer");
             
               billsMD = _db.Bills.Where(b => b.CustomerNameForBill.Contains(billsSearch.CustomerName) && b.FirstAuditFromCompanyEmployee == true && b.SecondAuditFromCompanyEmployee == true 
                                              && b.IsInsured == true).OrderByDescending(b => b.BillDate).ToList();

           
                return billsMD; 
                

            }  


            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName != null ) 
            {  
                Console.WriteLine("Company");
             
               billsMD = _db.Bills.Where(b => b.MerchantBussinessName.Contains(billsSearch.CompanyName) && b.FirstAuditFromCompanyEmployee == true && b.SecondAuditFromCompanyEmployee == true 
                                              && b.IsInsured == true).OrderByDescending(b => b.BillDate).ToList();

              
                return billsMD; 
                

            }  


            if(billsSearch.EndDate.ToString() != "none"  && billsSearch.StartDate.ToString() != "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Date");
               
              billsMD = _db.Bills.Where(b => b.BillDate > Convert.ToDateTime(billsSearch.StartDate)  && b.BillDate < Convert.ToDateTime(billsSearch.EndDate) && b.FirstAuditFromCompanyEmployee == true && b.SecondAuditFromCompanyEmployee == true 
                                              && b.IsInsured == true ).OrderByDescending(b => b.BillDate).ToList();

              
                return billsMD; 
                

            } 

            

            else {
                Console.WriteLine("null");
                return null;
                
            } 

            }

            if(billsSearch.RoleForSearch == "CompanyTwo") { 
                  List<Bill> billsC1 ;   


              if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null && billsSearch.last24Hours == "true") 
                { 
                    billsC1 = _db.Bills.Where(b => b.BillDate >= DateTime.Now.AddHours(-24) && b.BillDate <= DateTime.Now.AddHours(24) ).ToList(); 
                    Console.WriteLine("jjjj");
                    return billsC1; 

                }



            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("AllC1");
                billsC1 = _db.Bills.Where(b => b.FirstAuditFromCompanyEmployee == true && b.IsInsured == true).OrderByDescending(b => b.BillDate).ToList();  
                Console.WriteLine(billsC1);
                return billsC1; 
                

            }   


            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName != null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Merchant");
             
               billsC1 = _db.Bills.Where(b => b.MerchantNameForBill.Contains(billsSearch.MerchantName) &&  b.FirstAuditFromCompanyEmployee == true && b.IsInsured == true).OrderByDescending(b => b.BillDate).ToList();

        
                return billsC1; 
                

            }  

            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName != null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Customer");
             
               billsC1 = _db.Bills.Where(b => b.CustomerNameForBill.Contains(billsSearch.CustomerName) &&  b.FirstAuditFromCompanyEmployee == true && b.IsInsured == true).OrderByDescending(b => b.BillDate).ToList();

           
                return billsC1; 
                

            }  


            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName != null ) 
            {  
                Console.WriteLine("Company");
             
               billsC1 = _db.Bills.Where(b => b.MerchantBussinessName.Contains(billsSearch.CompanyName) &&  b.FirstAuditFromCompanyEmployee == true && b.IsInsured == true).OrderByDescending(b => b.BillDate).ToList();

              
                return billsC1; 
                

            }  


            if(billsSearch.EndDate.ToString() != "none"  && billsSearch.StartDate.ToString() != "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Date");
               
              billsC1 = _db.Bills.Where(b => b.BillDate > Convert.ToDateTime(billsSearch.StartDate)  && b.BillDate < Convert.ToDateTime(billsSearch.EndDate) &&  b.FirstAuditFromCompanyEmployee == true && b.IsInsured == true ).OrderByDescending(b => b.BillDate).ToList();

              
                return billsC1; 
                

            } 

            

            else {
                Console.WriteLine("null");
                return null;
                
            } 



            } 

            if (billsSearch.RoleForSearch == "CompanyOne") {  

                List<Bill> billsC2; 


            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null && billsSearch.last24Hours == "true") 
                { 
                    billsC2 = _db.Bills.Where(b => b.BillDate >= DateTime.Now.AddHours(-24) && b.BillDate <= DateTime.Now.AddHours(24) ).ToList(); 
                    Console.WriteLine("jjjj");
                    return billsC2; 

                }





            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("AllC2");
                billsC2 = _db.Bills.OrderByDescending(b => b.BillDate).ToList();  
                Console.WriteLine(billsC2);
                return billsC2; 
                

            }   


            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName != null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Merchant");
             
               billsC2 = _db.Bills.Where(b => b.MerchantNameForBill.Contains(billsSearch.MerchantName)).OrderByDescending(b => b.BillDate).ToList();

        
                return billsC2; 
                

            }  

            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName != null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Customer");
             
               billsC2 = _db.Bills.Where(b => b.CustomerNameForBill.Contains(billsSearch.CustomerName)).OrderByDescending(b => b.BillDate).ToList();

           
                return billsC2; 
                

            }  


            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName != null ) 
            {  
                Console.WriteLine("Company");
             
               billsC2 = _db.Bills.Where(b => b.MerchantBussinessName.Contains(billsSearch.CompanyName)).OrderByDescending(b => b.BillDate).ToList();

              
                return billsC2; 
                

            }  


            if(billsSearch.EndDate.ToString() != "none"  && billsSearch.StartDate.ToString() != "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Date");
               
              billsC2 = _db.Bills.Where(b => b.BillDate > Convert.ToDateTime(billsSearch.StartDate)  && b.BillDate < Convert.ToDateTime(billsSearch.EndDate) ).OrderByDescending(b => b.BillDate).ToList();

              
                return billsC2; 
                

            } 

            

            else {
                Console.WriteLine("null");
                return null;
                
            } 


                
            }

            // var a = billsSearch.StartDate; 
            // var b = billsSearch.EndDate;
            // var c = billsSearch.CustomerName;
            // var d = billsSearch.MerchantName;
            // var e = billsSearch.CompanyName;
            // var f = billsSearch.last24Hours ; 
            // if(a.ToString() == DateTime.MinValue.ToString())
            // Console.WriteLine("a"); 
            // if(b.ToString() == DateTime.MinValue.ToString())
            // Console.WriteLine("b"); 
            // if(c == "")
            // Console.WriteLine("c"); 
            // if(d  == "")
            // Console.WriteLine("d"); 
            // if(e == "")
            // Console.WriteLine("e"); 
            // if(f == false)
            // Console.WriteLine("f");
            List<Bill> bills ; 
          //  Console.WriteLine(billsSearch.CompanyName);  
          //  var c = billsSearch.CompanyName; 
       
            

            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("All");
                bills = _db.Bills.OrderByDescending(b => b.BillDate).ToList();  
                Console.WriteLine(bills);
                return bills; 
                

            }   


            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName != null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Merchant");
               // bills = _db.Bills.OrderByDescending(b => b.BillDate).ToList();  
               bills = _db.Bills.Where(b => b.MerchantNameForBill.Contains(billsSearch.MerchantName)).OrderByDescending(b => b.BillDate).ToList();

               // Console.WriteLine(bills);
                return bills; 
                

            }  

            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName != null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Customer");
               // bills = _db.Bills.OrderByDescending(b => b.BillDate).ToList();  
               bills = _db.Bills.Where(b => b.CustomerNameForBill.Contains(billsSearch.CustomerName)).OrderByDescending(b => b.BillDate).ToList();

               // Console.WriteLine(bills);
                return bills; 
                

            }  


            if(billsSearch.EndDate.ToString() == "none"  && billsSearch.StartDate.ToString() == "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName != null ) 
            {  
                Console.WriteLine("Company");
               // bills = _db.Bills.OrderByDescending(b => b.BillDate).ToList();  
               bills = _db.Bills.Where(b => b.MerchantBussinessName.Contains(billsSearch.CompanyName)).OrderByDescending(b => b.BillDate).ToList();

               // Console.WriteLine(bills);
                return bills; 
                

            }  


            if(billsSearch.EndDate.ToString() != "none"  && billsSearch.StartDate.ToString() != "none" && billsSearch.MerchantName == null 
              && billsSearch.CustomerName == null && billsSearch.CompanyName == null ) 
            {  
                Console.WriteLine("Date");
               // bills = _db.Bills.OrderByDescending(b => b.BillDate).ToList();  
              bills = _db.Bills.Where(b => b.BillDate > Convert.ToDateTime(billsSearch.StartDate)  && b.BillDate < Convert.ToDateTime(billsSearch.EndDate) ).OrderByDescending(b => b.BillDate).ToList();

               // Console.WriteLine(bills);
                return bills; 
                

            } 

            

            else {
                Console.WriteLine("null");
                return null;
                
            } 




        }  

         public List<Bill> GetAllBillsMerchant(string MerchantAcctNum)
         { 
            List<Bill> billsForMerchant = _db.Bills.Where(b => b.MerchantAcctNumForBill == MerchantAcctNum).ToList(); 
            return billsForMerchant; 

         }   

           public List<Bill> GetRejectedBillsMerchant(string MerchantAcctNum)
         { 
           // List<Bill> billsForMerchant = _db.Bills.Where(b => b.MerchantAcctNumForBill == MerchantAcctNum && b.FirstAuditFromCompanyEmployee == false && (b.NoteForRejection != "" || b.NoteForRejection != null )).ToList(); 

           List<Bill> billsForMerchant = _db.Bills.Where(b => b.MerchantAcctNumForBill == MerchantAcctNum && 
                                                              ( (b.NoteForInsuranceRejection != null && b.IsInsured == false) || 
                                                               (b.NoteForRejection != null && b.FirstAuditFromCompanyEmployee == false)  || 
                                                              (b.NoteForOperationRejection != null && b.SecondAuditFromCompanyEmployee == false) || 
                                                              (b.DirectiveManagerRejectionNote != null && b.DirectiveManagerApproval == false))).ToList();  

            // List<Bill> billsForMerchant = _db.Bills.Where(b => b.MerchantAcctNumForBill == MerchantAcctNum && (
            //  (b.FirstAuditFromCompanyEmployee == false && (b.NoteForRejection != "" || b.NoteForRejection != null )) || 
            //  (b.IsInsured == false && (b.NoteForInsuranceRejection != "" || b.NoteForInsuranceRejection != null)) || 
            //  (b.SecondAuditFromCompanyEmployee == false && (b.NoteForOperationRejection != "" || b.NoteForOperationRejection != null))
            // )
            // ).ToList();  
            // List<Bill> billsForMerchant = _db.Bills.Where(
            //     b => (b.MerchantAcctNumForBill == MerchantAcctNum  && b.IsInsured == false && b.NoteForInsuranceRejection != null )
            //     || (b.MerchantAcctNumForBill == MerchantAcctNum  && b.FirstAuditFromCompanyEmployee == false && b.NoteForRejection != null )
            //     || (b.MerchantAcctNumForBill == MerchantAcctNum  && b.SecondAuditFromCompanyEmployee == false && b.NoteForOperationRejection != null) 
            // ).ToList();
            return billsForMerchant; 

         }   

        public List<Bill> GetAllBillsInsurance()
        { 
            List<Bill> billsForInsurance = _db.Bills.Where(b => b.FirstAuditFromCompanyEmployee == true && b.IsInsured == false && (b.NoteForInsuranceRejection == null || b.NoteForInsuranceRejection == "") ).OrderByDescending(b => b.BillDate).ToList(); 
            return billsForInsurance; 

        }   


        public List<Bill> GetAllBillsInsSuc()
        { 
            List<Bill> billsInsurance = _db.Bills.Where(b => b.FirstAuditFromCompanyEmployee == true && b.IsInsured == true).OrderByDescending(b => b.BillDate).ToList(); 
            return billsInsurance; 

        }  


          public List<PartialPay> GetPartialPayByBill (string billSeq)
          {
              List<PartialPay> listPartialPay = _db.PartialPays.Where(pp => pp.BillSeq == billSeq).OrderBy(pp => pp.Num).ToList(); 

              return listPartialPay;

          } 


           public Bill GetBillById (string billSeq)
           {
               if(billSeq != null){
               Bill billById = _db.Bills.FirstOrDefault(b => b.BillSeq == billSeq); 
               return billById;
               }
               else {
                   return null;
               }

               

           } 

            


        //   public List<PartialPay> GetPartialPayByBill(Bill bill) 
        //   {
        //       List<PartialPay> partialPays = new List<PartialPay>(); 

        //       partialPays = _db.PartialPays.Where( p => p.BillSeq == bill.BillSeq).ToList();

        //   }

            

        

        // public List<Good> GetAllGoods(GoodsGet goodsGet)
        // {
        //     List<Good> goods = _db.Goods.Where(g => g.CustomerAcctNum == goodsGet.CustomerAcctNum && g.MerchantAcctNum == goodsGet.MerchantAcctNum).ToList();
        //     Console.WriteLine(goods);
        //     //  List<Good> goods = _db.Goods.ToList();   


        //     return goods;

        // } 

       
    }
}
