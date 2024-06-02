function openSideNavbar() {
    document.getElementById("mySidenav").style.display = "block";
}

function closeSideNavbar() {
    document.getElementById("mySidenav").style.display = "none";
}



function OpenWarePage(id) {
    $.ajax({
        method: "GET",
        url: "/Home/WarePage/" + id,
        success: function (data) {
            $("#mainSection").html(data);
        }
    });
}

function BackToHome() {
    $.ajax({
        method: "GET",
        url: "/Home/Home",
        success: function (data) {
            $("#mainSection").html(data);
        }
    });
}


function Search() {
    $.ajax({
        method: "GET",
        url: "/Home/Search/",
        data: { query: $("#searchInput").val() },
        success: function (data) {
            $("#mainSection").html(data);
        },

    })
}

function AddToBasket(id) {
    $.ajax({
        method: "POST",
        url: "/ShopingCart/AddToBasket",
        data: { "wareId": id },
        success: function (data) {
            $("#basketSection").html(data)
        },
        error: function (error) {
            $.ajax({
                method: "GET",
                url: "/Account/Login",
                success: function (data) {
                    $("#loginModal").modal('show');
                    $(".modal-body").html(data);
                    console.clear();
                }
            })
        }

    })
}


function OpenBasket() {
    $.ajax({
        method: "GET",
        url: "/ShopingCart/Index",
        success: function (data) {
            $("#bigModal").modal('show');
            $(".modal-body").html(data);
        },

    })
}

function RemoveFromShopingCart(id) {
    var wareCount = Number.parseInt($("#wareCount").html())

    $.ajax({
        method: "GET",
        url: "/ShopingCart/Remove",
        data: { "wareId": id },
        success: function (data) {
            $(".modal-body").html(data);
            wareCount--;
            $("#wareCount").html(wareCount)
        },

    })
}


function DeleteFromShopingCart(id, beforeCount) {
    var wareCount = Number.parseInt($("#wareCount").html())

    $.ajax({
        method: "POST",
        url: "/ShopingCart/Delete",
        data: { "shopingCartId": id },
        success: function (data) {
            $(".modal-body").html(data);
            wareCount -= beforeCount;
            $("#wareCount").html(wareCount);
        },

    })
}




function OpenRegisterPage() {
    $.ajax({
        method: "GET",
        url: "/Account/Register",
        success: function (data) {
            $("#signinModal").modal('show');
            $(".modal-body").html(data);
        },
    })
}


function OpenLoginPage() {
    $.ajax({
        method: "GET",
        url: "/Account/Login",
        success: function (data) {
            $("#loginModal").modal('show');
            $(".modal-body").html(data);
        },
    })
}

function Logout() {
    $.ajax({
        method: "POST",
        url: "/Account/Logout",
        success: function () {
            location.reload();
        },
    })
}





