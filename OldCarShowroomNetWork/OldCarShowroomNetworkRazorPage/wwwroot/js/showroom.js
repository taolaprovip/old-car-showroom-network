var connection = new signalR.HubConnectionBuilder().withUrl("/showroomHub").build();

connection.start().then(function () {
    console.log("SignalR connected.");
    connection.invoke("GetAllShowrooms");
}).catch(function (err) {
    console.error(err.toString());
});

connection.on("ReceiveAllShowrooms", function (showrooms) {
    console.log("Received showrooms:", showrooms);

    var showroomList = $("#showroomList");
    showroomList.empty();

    for (var i = 0; i < showrooms.length; i++) {
        (function () {
            var showroom = showrooms[i];
            var showroomId = showroom.showroomId;

            connection.invoke("GetShowroomImageUrl", showroomId).then(function (imageUrl) {
                // Xử lý imageUrl trả về để hiển thị hình ảnh trong img tag
                var col = $("<div class='col mb-5'></div>");
                var card = $("<div class='card h-100'></div>");
                var img = $("<img class='card-img-top' src='" + imageUrl + "' alt='...' />");
                var cardBody = $("<div class='card-body p-4'></div>");
                var textCenter = $("<div class='text-center'></div>");
                var name = $("<h5 class='fw-bolder'>" + showroom.showroomName + "</h5>");
                var address = $("<p>" + showroom.address + "</p>");
                var buttonContainer = $("<div class='d-flex justify-content-center'></div>"); // Thêm class 'd-flex justify-content-between'
                var deleteButton = $("<button class='btn btn-danger'>Delete</button>");
                var editButton = $("<button class='btn btn-primary'>Edit</button>");

                textCenter.append(name);
                textCenter.append(address);
                cardBody.append(textCenter);
                buttonContainer.append(deleteButton);
                buttonContainer.append(editButton);
                cardBody.append(buttonContainer);
                card.append(img);
                card.append(cardBody);
                col.append(card);

                showroomList.append(col);
            }).catch(function (err) {
                console.error(err.toString());
            });
        })();
    }
});

function loadDistricts(cityId) {
    // Xóa các tùy chọn quận và phường hiện có
    $("#Showroom_DistrictId").empty();
    $("#Showroom_Wards").empty();

    // Gửi yêu cầu tải danh sách quận dựa trên thành phố đã chọn
    connection.invoke("LoadDistricts", cityId).then(function () {
        // Lấy quận đã chọn
        var selectedDistrictId = $("#Showroom_DistrictId").val();

        // Gọi phương thức LoadWards để tải danh sách phường dựa trên quận đã chọn
        if (selectedDistrictId) {
            connection.invoke("LoadWards", selectedDistrictId);
        }
    });
}
// Cập nhật danh sách quận khi nhận được phản hồi từ server
connection.on("LoadDistrictsResponse", function (districts) {
    // Xóa các option hiện tại
    $("#Showroom_DistrictId").empty();
    // Thêm option cho từng quận
    $.each(districts, function (i, district) {
        $("#Showroom_DistrictId").append($('<option></option>').val(district.districtId).text(district.name));
    });
});
connection.on("LoadWardsResponse", function (wards) {
    // Xóa các tùy chọn phường hiện có
    $("#Showroom_Wards").empty();

    // Thêm các tùy chọn cho từng phường
    $.each(wards, function (i, ward) {
        $("#Showroom_Wards").append($('<option></option>').val(ward.wardId).text(ward.name));
    });
});
