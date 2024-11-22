using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.AuthorizationResponse;
using E_ecommerce.Data.DTO.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Authorization.Commands.Models
{
    public class IsUserExistInRoleModel : IRequest<Response<IsExist>>
    {
        [Required]
        public int Role_Id { get; set; }
        [Required]
        public int User_Id { get; set; }

        public IsUserExistInRoleModel(int role_Id, int user_Id)
        {
            Role_Id = role_Id;
            User_Id = user_Id;
        }
    }
}
