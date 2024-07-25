using ArganaWeedClietApi.Models;
using ArganaWeedClietApi.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedClietApi
{
    public class ItemResponse :BaseResponse
    {
        public List<Item> items { get; set; }
    }
}
