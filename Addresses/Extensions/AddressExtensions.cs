
using Microsoft.EntityFrameworkCore;
using QuickFix.Addresses.Data;
using QuickFix.Addresses.Models;
using QuickFix.Shared.Module;

namespace QuickFix.Addresses.Extensions
{
    public static class AddressExtensions
    {
        public static async Task<IEnumerable<AddressModel>> FindAllAddressAsync(
this IAddressDbContext _address
)
        {
            return await _address.address.ToListAsync();
        }
        public static async Task<AddressModel> FindCompanyAddressAsync(
        this IAddressDbContext _address,
        Guid? companyAddressId
        )
        {
            return await _address.address.FirstOrDefaultAsync(a => a.Id == companyAddressId);
        }
        public static async Task<AddressModel> FindAddressByIdAsync(
        this IAddressDbContext _address,
        Guid Id
        )
        {
            return await _address.address.FirstOrDefaultAsync(a => a.Id == Id);
        }
        public static async Task<AddressModel> FindBranchAddressAsync(
        this IAddressDbContext _address,
        Guid? branchAddressId
        )
        {
            return await _address.address.FirstOrDefaultAsync(a => a.Id == branchAddressId);
        }
        public static async Task<IEnumerable<AddressModel>> FindAllUserAddressAsync(
        this IAddressDbContext _address,
        Guid? userAddressId
        )
        {
            return await _address.address.ToListAsync();
        }
        public static async Task<AddressModel> FindAddressByUserIdAsync(
     this IAddressDbContext _address,
     Guid userId
     )
        {
            return await _address.address.FirstOrDefaultAsync(a => a.Id == userId);
        }
        public static async Task<AddressModel> FindUserAddressAsync(
        this IAddressDbContext _address,
        Guid? userAddressId
        )
        {
            return await _address.address.FirstOrDefaultAsync(a => a.Id == userAddressId);
        }
        public static async Task<DataRespons> CreateAddressAsync(
        this IAddressDbContext _address,
        AddressModel address,
        CancellationToken cancellationToken
        )
        {
            _address.address.Add(address);
            try
            {
                await _address.SaveChangesAsync(cancellationToken);
                return new DataRespons
                {
                    Id = address.Id,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new DataRespons
                {
                    Id = address.Id,
                    Message = ex.Message,
                    StatusCode = 400
                };
            }
        }
        public static async Task<DataRespons> UpdateAddressAsync(
            this IAddressDbContext _address,
            AddressModel address
        )
        {
            try
            {
                _address.address.Update(address);
                await _address.SaveChangesAsync();
                return new DataRespons
                {
                    Id = address.Id,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new DataRespons
                {
                    Id = address.Id,
                    Message = ex.Message,
                    StatusCode = 400
                };
            }
        }
        /// <summary>
        /// delete address
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static async Task<DataRespons> DeleteAddressAsync(
        this IAddressDbContext _address,
        AddressModel address
        )
        {
            try
            {
                _address.address.Remove(address);
                await _address.SaveChangesAsync();
                return new DataRespons
                {
                    Id = address.Id,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new DataRespons
                {
                    Id = address.Id,
                    Message = ex.Message,
                    StatusCode = 400
                };
            }
        }
        /// <summary>
        /// delete address by id
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static async Task<DataRespons> DeleteAddressByIdAsync(
        this IAddressDbContext _address,
        Guid addressId
        )
        {
            try
            {
                var address = await _address.address.FirstOrDefaultAsync(a => a.Id == addressId);
                _address.address.Remove(address);
                await _address.SaveChangesAsync();
                return new DataRespons
                {
                    Id = address.Id,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new DataRespons
                {
                    Id = addressId,
                    Message = ex.Message,
                    StatusCode = 400
                };
            }
        }

        /// <summary>
        /// delete all address
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static async Task<DataRespons> DeleteAddressByRangeAsync(
        this IAddressDbContext _address,
        IEnumerable<Guid> Id
        )
        {
            try
            {
                var address = await _address.address.Where(a => Id.Contains(a.Id)).ToListAsync();
                _address.address.RemoveRange(address);
                await _address.SaveChangesAsync();
                return new DataRespons
                {
                    Id = Guid.NewGuid(),
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new DataRespons
                {
                    Id = Guid.NewGuid(),
                    Message = ex.Message,
                    StatusCode = 400
                };
            }
        }
    }
}
