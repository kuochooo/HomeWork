using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HomeWork.Models
{   
	public  class 客戶數量資訊Repository : EFRepository<客戶數量資訊>, I客戶數量資訊Repository
	{

	}

	public  interface I客戶數量資訊Repository : IRepository<客戶數量資訊>
	{

	}
}