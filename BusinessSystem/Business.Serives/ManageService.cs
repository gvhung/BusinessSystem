﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Business.Core;
using Business.Nhibernate.IRepository;
using Business.Nhibernate.Repository;

namespace Business.Serives
{
     public  class ManageService
     {
         private static  IManagerRepository managerRepository=new ManagerRepository();

         public static IList<Manager> GetManagersByPage(ManagerTypeEnum managerType,int pageIndex,int pageSize,long parentId)
         {
             return managerRepository.GetManagersByPage(managerType,pageIndex,pageSize,parentId);
         }

         public static Manager GetSuperManager()
         {
             return managerRepository.GetSuperManager();
         }

         public static Manager GetManagerById(long Id)
         {
             return managerRepository.GetById(Id);
         }

         public static string Login(string userName, string password,out Manager resultManager)
         {
             Manager manager = managerRepository.GetManagerByUserName(userName);
             resultManager = manager;
             if (manager == null)
             {
                 return ResponseCode.Managaer.UserNameError;
             }
             if (!manager.MatchPassword(password))
             {
                 return ResponseCode.Managaer.UserPasswordError;
             }
             return ResponseCode.Ok;
         }

         public static string Save(Manager manager)
         {
             if (string.IsNullOrEmpty(manager.UserName))
             {
                 return ResponseCode.Managaer.UserNullOrEmpty;
             }
             if (string.IsNullOrEmpty(manager.Password))
             {
                 return ResponseCode.Managaer.PasswordNullOrEmpty;
             }
             if (manager.Language==0)
             {
                 return ResponseCode.Managaer.LanguageNullOrEmpty;
             }
             if (string.IsNullOrEmpty(manager.RealName))
             {
                 return ResponseCode.Managaer.RealNameNullOrEmpty;
             }
             if (string.IsNullOrEmpty(manager.Company))
             {
                 return ResponseCode.Managaer.CompanyNullOrEmpty;
             }
             if (managerRepository.IsExist(manager))
             {
                 return ResponseCode.Managaer.UserNameHasExist;
             }
             managerRepository.Save(manager);
             return ResponseCode.Ok;
         }

         public static void DeleteManager(long id)
         {
             Manager manager = managerRepository.GetById(id);
             if (manager != null)
             {
                 managerRepository.Delete(manager);
             }
         }

         public static void ResetPassword(long id)
         {
             Manager manager = managerRepository.GetById(id);
             if (manager != null)
             {
                 manager.Password = "123456";
                 manager.EncryptPassword();
                 managerRepository.Save(manager);
             }
         }

         public static string ChangePassword(Manager manager, string oldPassword, string newPassword, string confirmPassword)
         {
             if (string.IsNullOrEmpty(oldPassword))
             {
                 return ResponseCode.Managaer.OldPasswordNullOrEmpty;
             }
             if (string.IsNullOrEmpty(newPassword))
             {
                 return ResponseCode.Managaer.NewPasswordNullOrEmpty;
             }
             if (newPassword!=confirmPassword)
             {
                 return ResponseCode.Managaer.ConfirmPasswordError;
             }
             Manager manage = managerRepository.GetById(manager.Id);
             if (manage != null)
             {
                 if (!manage.MatchPassword(oldPassword))
                 {
                     return ResponseCode.Managaer.OldPasswordError;
                 }
                 manage.Password = newPassword;
                 manage.EncryptPassword();
                 managerRepository.Save(manage);
                 manager = manage;
                 return ResponseCode.Ok;
             }
             return ResponseCode.NotFoundData;
         }
     }
}
