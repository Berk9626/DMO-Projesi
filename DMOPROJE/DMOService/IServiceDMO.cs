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
    
    [ServiceContract]
    public interface IServiceDMO
    {
        [OperationContract]
        ICollection<ProductsDTO> GetAllProducts(int? supplierId, string role);

        [OperationContract]
        UserDTO Login(string userID, string paswword);

        [OperationContract]
        string DeleteProduct(int id,string role,int supplierId);

        [OperationContract]
        string AddProduct(string role, ProductsDTO p);




    }
}
