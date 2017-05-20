using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using HomeWork.Models;
using System.Web.Mvc;

namespace HomeWork.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => !p.Is刪除);
        }      

        public 客戶資料 Get單筆資料ByProductId(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<客戶資料> search資料(string search_str,int searchState)
        {   
            if (searchState == 1)
            { return this.Where(q => q.統一編號.Contains(search_str)); }
            if (searchState == 2)
            { return this.Where(q => q.電話.Contains(search_str)); }
            return this.Where(q => q.客戶名稱.Contains(search_str));
        }

        public List<SelectListItem> 客戶管理清單()
        {
            List<SelectListItem> selectlist = new List<SelectListItem>();

            foreach (var q in this.All())
            {
                selectlist.Add(new SelectListItem() { Text = q.客戶分類, Value = q.客戶分類 });
            }

            return selectlist;
        }

        public IQueryable<客戶資料> search客戶分類(string 客戶分類)
        {
            return this.All().Where(a => a.客戶分類 == 客戶分類);
        }

    public IQueryable<客戶資料> GetProduct列表頁所有資料()
        {
            IQueryable<客戶資料> all = this.All();
            return all;           
        }

        public void Update(客戶資料 product)
        {
            this.UnitOfWork.Context.Entry(product).State = EntityState.Modified;
        }
    }

}

public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}

