using BOs.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace OldCarShowroomNetworkRazorPage.Pages.Hubs
{
    public class ShowroomHub : Hub
    {
        private readonly OldCarShowroomNetworkContext _dbContext;
        public ShowroomHub()
        {
            _dbContext = new OldCarShowroomNetworkContext();
        }
        public async Task GetAllShowrooms()
        {
            // Get all the Car data from the database
            List<BOs.Models.Showroom> showrooms = await _dbContext.Showrooms.ToListAsync();
            
            // Send the Car data to the clients
            await Clients.All.SendAsync("ReceiveAllShowrooms", showrooms);
        }
        public string GetShowroomImageUrl(int showroomId)
        {
            var imageShowroom = _dbContext.ImageShowrooms.FirstOrDefault(x => x.Showrooms.Any(s => s.ShowroomId == showroomId));
            string? imageUrl = imageShowroom?.Url;
            return imageUrl;
        }
        public async Task LoadDistricts(int cityId)
        {
            
            
                // Tải danh sách các quận dựa trên thành phố đã chọn
                var districts = await _dbContext.Districts.Where(d => d.CityId.Equals(cityId)).ToListAsync();

                // Gửi danh sách quận tới client
                await Clients.Caller.SendAsync("LoadDistrictsResponse", districts);

                // Xóa danh sách phường
                await Clients.Caller.SendAsync("ClearWardsDropdown");
            
        }
        public async Task LoadWards(int districtId)
        {
            // Tải danh sách phường dựa trên quận đã chọn
            var wards = await _dbContext.Wards.Where(w => w.DistrictId.Equals(districtId)).ToListAsync();

            // Gửi danh sách phường tới client
            await Clients.Caller.SendAsync("LoadWardsResponse", wards);
        }
    }
}
