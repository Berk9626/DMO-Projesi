using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DMOService
{

    public class ServiceDMO : IServiceDMO
    {

        NorthwindEntities db = new NorthwindEntities();

        public string AddProduct(string role, ProductsDTO p)
        {
            if (role == "TED")
            {
                Products products = new Products();
                products.ProductName = p.ProductName;
                products.UnitPrice = p.UnitPrice;
                products.SupplierID = p.SupplierID;
                products.Discontinued = false;
                db.Products.Add(products);
               // db.Entry(products).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return products.ProductName + "Eklendi.";

            }
            else
            {
                return "Kamu personeli ürün ekleyemez";
            }
        }

        public string DeleteProduct(int id,string role,int supplierId)//
        {
            Products p = db.Products.Find(id);
            

            if (role == "TED")
            {

                if (supplierId == p.SupplierID)
                {
                    p.Discontinued = true;
                    db.SaveChanges();
                    return p.ProductName + "silindi";

                }
                else
                {
                    return "Başkasının ürününü silemezsin";
                }
            }

            else
            {
                return "kamu ürün silemez";
            }
            
        }



        public ICollection<ProductsDTO> GetAllProducts(int? supplierId, string role)
        {
            if (supplierId == null)
            {
                supplierId = 0;

            }
            if (role == "KAMU")
            {
                return db.Products.Select(x => new ProductsDTO
                {
                    ProductsID = x.ProductID,
                    ProductName = x.ProductName,
                    SupplierID = (int)x.SupplierID,
                    Discontinued = x.Discontinued,
                    UnitPrice = (decimal)x.UnitPrice

                }).Where(x => x.Discontinued == false).ToList();


            }

            else
            {
                return db.Products.Select(x => new ProductsDTO
                {
                    ProductsID = x.ProductID,
                    ProductName = x.ProductName,
                    SupplierID = (int)x.SupplierID,
                    Discontinued = x.Discontinued,
                    UnitPrice = (decimal)x.UnitPrice

                }).Where(x => x.SupplierID == supplierId ).ToList();
            }

        }

        public UserDTO Login(string userID, string paswword)
        {
            UserDTO userDTO = new UserDTO();

            Users u = db.Users.Find(userID);
            if (u == null)
            {
                return null;

            }

            else  
            {
                if(u.UserID ==userID && u.Password == paswword)
                {
                      userDTO.UserID = u.UserID  ;
                      userDTO.Role = u.Role;
                    try
                    {//Hata veriyor yoksa.
                        userDTO.SupplierID = (int)u.SupplierID;
                    }
                    catch (Exception)
                    {

                        userDTO.SupplierID = 0;
                    }
                     

                    return userDTO;
                }
                else
                {
                    return null;
                }
                
            }
        }
    }
}
