﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using SuportAPI.Data;

namespace SuportAPI.API.User
{
    partial class UserController
    {
        [HttpGet("getUser")]
        public async Task<ActionResult<VMs.User>> GetUser(int id)
        {
            try
            {
                // QUERY
                var dataUser = await context.Users
                    .Where(x => x.RowStatus == Data.enRowStatus.Active && x.Id == id)
                    .FirstOrDefaultAsync();

                // MODELING
                var result = ConvertVMUser(dataUser);                

                // RESULT
                return OkResponse(result);
            }
            catch(Exception ex) { return BadRequestResponse(ex); }
        }

        public async Task<VMs.User> GetOwner(int id)
        {
            // QUERY
            var dataUser = await context.Users
                .Where(x => x.RowStatus == Data.enRowStatus.Active && x.Id == id)
                .FirstOrDefaultAsync();

            // MODELING
            var result = ConvertVMUser(dataUser);

            return result;
        }

        private VMs.User ConvertVMUser(Data.User user)
        {
            VMs.User result = new VMs.User();

            try
            {
                result.Id = user.Id;
                result.Name = user.Name;
                result.Type = user.Type.ToString();
                result.Login = user.Login;
                result.Password = user.Password;
                result.Company = user.Company;
                               
                return result;
            }
            catch(Exception ex) { throw ex; }
            
        }


    }
}