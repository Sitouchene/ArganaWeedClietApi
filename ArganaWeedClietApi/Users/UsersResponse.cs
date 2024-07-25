using ArganaWeedClietApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedClietApi.Users
{
    public class UsersResponse: BaseResponse
    {
        public List <User> users { get; set; }
    }
}
