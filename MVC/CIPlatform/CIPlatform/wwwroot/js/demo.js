$(".close-chips").on("click", function (e) {
    $(".home-chips .chip").remove();
    $(this).hide();
});

// $(".close-chips").on("click", function(e){
// $(".close-chips").remove();
// });
function showChip() {
    $(".selection .dropdown-menu li a").on("click", function (e) {
        $(".home-chips .chips").append(
            '<div class="chip">' +
            $(this).text() +
            '<span class="closebtn" onclick="this.parentElement.style.display=\'none\'">&times;</span>'
        );
        $(".close-chips").show();
    });
}


$("#mission-list").hide();
$("#select-list-style").on("click", function (e) {
    $(".grid-view").hide();
    $("#mission-list").show();
    localStorage.setItem("lastVisible", "list");
});

$("#select-grid-style").on("click", function (e) {
    $("#mission-list").hide();
    $(".grid-view").show();
    localStorage.setItem("lastVisible", "grid");
})


    /*AJAX Call for Countries - Dropdown*/

$(document).ready(function () {
    /*debugger*/
    var ddlCountry = $('#mission-countries');
    ddlCountry.append($("<ul></ul>").val(''));
    var ajaxRequest1 = $.ajax({
        url: 'https://localhost:7165/Mission/listCountries',
        type: 'GET',
        dataType: 'json',
        success: function (d) {
            /*debugger*/
            var a = "";
            $.each(d, function (i, country) {
                a += '<li><a class= "dropdown-item" href = "#" >' + country.name + '</a></li>';
            });
            ddlCountry.append(a);
        },
        error: function () {
            alert('Error!');
        }
    });

    /*AJAX Call for Cities - Dropdown*/

    var ddlCity = $('#mission-cities');
    ddlCity.append($("<ul></ul>").val(''));
    var ajaxRequest2 = $.ajax({
        url: 'https://localhost:7165/Mission/listCities',
        type: 'GET',
        dataType: 'json',
        success: function (d) {
            var b = "";
            $.each(d, function (i, city) {
                b += '<li><a class= "dropdown-item" href = "#" >' + city.name + '</a></li>';
            });
            ddlCity.append(b);
        },
        error: function () {
            alert('Error!');
        }
    });

    /*AJAX Call for Theme - Dropdown*/

    var ddltheme = $('#mission-theme');
    ddltheme.append($("<ul></ul>").val(''));
    var ajaxRequest3 = $.ajax({
        url: 'https://localhost:7165/Mission/listTheme',
        type: 'GET',
        dataType: 'json',
        success: function (d) {
            var c = "";
            $.each(d, function (i, theme) {
                c += '<li><a class= "dropdown-item" href = "#" >' + theme.title + '</a></li>';
            });
            ddltheme.append(c);
        },
        error: function () {
            alert('Error!');
        }
    });

    /*AJAX Call for Skill - Dropdown*/

    var ddlSkill = $('#mission-skill');
    ddlSkill.append($("<ul></ul>").val(''));
    var ajaxRequest4 = $.ajax({
        url: 'https://localhost:7165/Mission/listSkill',
        type: 'GET',
        dataType: 'json',
        success: function (d) {
            var skills = "";
            $.each(d, function (i, skill) {
                skills += '<li><a class= "dropdown-item" href = "#" >' + skill.skillName + '</a></li>';
            });
            ddlSkill.append(skills);
        },
        error: function () {
            alert('Error!');
        }
    });

    $.when(ajaxRequest1, ajaxRequest2, ajaxRequest3, ajaxRequest4).done(function () {
        showChip();
    });
});
    /*AJAX Call for Grid View*/

//    $.ajax({
//        type: "GET",
//        url: 'https://localhost:7165/Mission/GridSP',
//        dataType: "html",
//        success: function (data) {
//            $("#GridView").html("");
//            /*Loop through the data and append each item to the grid*/
//            /*var html = '';*/
//            /*console.log(data);*/
//            //$.each(data, function (i, item) {
//            //    //console.warn(typeof (isStringNullOrEmpty(item.startDate)))
//            //    /*console.log(item.countryName);*/
//            //     html += '<div class="grid-item col-sm-12 col-md-6 col-lg-4 mb-4" id="grid-column">'+
//            //        '<div class="card">' +
//            //        '<div class="card-image">' +
//            //        '<img class="card-img-top" src="' + item.missionImage + '" alt="Card image cap" />' +
//            //        '<div class="location">' +
//            //        '<img class="country-pin" src="/images/pin.png" alt="" />' +
//            //        '<span class="country">' + item.cityName + '</span>' +
//            //        '</div>' +
//            //        '<div class="feature">' +
//            //        '<img src="/images/heart.png" alt="" />' +
//            //        '<img src="/images/user.png" alt="" />' +
//            //        '</div>' +
//            //        '<p class="filter"><span>' + item.themeName + '</span></p>' +
//            //        '</div>' +
//            //        '<div class="card-body">' +
//            //        '<h5 class="card-title">' + item.missionTitle + '</h5>' +
//            //        '<div class="text-1">' +
//            //        '<p class="card-text">' + item.missionShortDesc.substring(0, 100) + "..." + '</p>' +
//            //        '</div>' +
//            //        '<div class="ratings">' +
//            //        '<span>' + item.organizationName + '</span>' +
//            //        '<div class="rating">';
//            //    for (var j = 0; j < 5; j++) {
//            //        html += '<i class="bi bi-star"></i>';
//            //    }
//            //    html += '</div>' +
//            //        '</div>' +
//            //        '</div>' +
//            //        '<hr />' +
//            //        '<p class="mission-date">' +
//            //        '<span class="time-span">From ' + (item.startDate === null || item.startDate === undefined) ? '' : item.startDate.split("T")[0] +'until'+ (item.endDate === null || item.endDate === undefined) ? '' : item.endDate.split("T")[0] + '</span>' +
//            //        '</p>' +
//            //        '<div class="card-body insight">' +
//            //        '<div class="card-body-left">' +
//            //        '<div class="card-body-left-img">' +
//            //        '<img src="/images/Already-volunteered.png" alt="" />' +
//            //        '</div>' +
//            //        '<div class="card-body-left-text">' +
//            //        '<span class="figure">' + item.availableSeats + '</span>' +
//            //        '<span class="seats-left">Seats left</span>' +
//            //        '</div>' +
//            //        '</div>' +
//            //        '<div class="card-body-right">' +
//            //        '<div class="card-body-right-img">' +
//            //        '<img src="/images/deadline.png" alt="" />' +
//            //        '</div>' +
//            //        '<div class="card-body-right-text">' +
//            //            '<span class="figure">' + (item.endDate === null || item.endDate === undefined) ? '' : item.endDate.split("T")[0] + '</span>' +
//            //        '<span class="deadline">Deadline</span>' +
//            //        '</div>' +
//            //        '</div>' +
//            //        '</div>' +
//            //        '<hr />' +
//            //        '<div class="apply-btn">' +
//            //        '<button>' +
//            //        'Apply <img src="/images/right-arrow.png" alt="" />' +
//            //        '</button>' +
//            //        '<br /><br />' +
//            //        '</div>' +
//            //        '</div>' + '</div>' ;
//            //});
            
//            //var html2 = " ";
//            //$.each(data, function (i, item) {
//            //    html2 += '<div id="list-card-parent" class="col-12 d-flex p-0 mb-4 border shadow"><div class="list-image-part position-relative"> <img src="' +
//            //        item.missionImage + '" class="list-img" alt = "" /><h5 class="list-card-theme-title bg-white rounded-5 p-2">'
//            //        + item.themeName + '</h5><div id="list-card-image-content" class="h-100 d-flex flex-column position-absolute p-2"><div id="list-location-image" class="list-img-content p-2 rounded-5"><img src="/images/pin.png" alt="">'
//            //        + item.cityName + '</div><div class="d-flex flex-column align-items-end"><div class="img-heart list-img-content p-2 rounded-5 m-1"><img src="/images/heart.png" alt=""></div><div id="list-user-img" class="list-img-content p-2 rounded-5 m-1"><img src="/images/user.png" alt=""></div></div></div></div><div class="list-content-part d-flex flex-column justify-content-around p-2"><h5 class="card-title fs-3">'
//            //        + item.missionTitle + '</h5><p class="card-text">'
//            //        + item.missionShortDesc + '...</p><div id="list-info-content" class="d-flex justify-content-between"><div><div id="list-star-content" class="d-flex align-items-center justify-content-between"><p class="my-auto">' +
//            //        item.organizationName + '</p><div class="d-flex align-items-center"><img src="/images/selected-star.png" alt=""><img src="/images/selected-star.png" alt=""><img src="/images/selected-star.png" alt=""><img src="/images/star.png" alt=""><img src="/images/star.png" alt=""></div></div><div class="list-card-button-apply m-1 p-1 w-100 "><button id="list-apply-btn" class="btn btn-outline-warning rounded-5 py-2 px-4 fs-5 ">Apply<img src="/images/right-arrow.png" class="mx-1 ms-3" alt=""></button></div></div><div><div id="list-mission-duration-div"><h5 id="list-card-time-duration-title" class="border bg-white rounded-5 m-auto fs-6 p-2">From '
//            //        + (item.startDate === null || item.startDate === undefined) ? '' : item.startDate.split("T")[0] +""+
//            //            (item.endDate === null || item.endDate === undefined) ? '' : item.endDate.split("T")[0] 
//            //        + '</h5></div><div id="list-seats-info-card" class="d-flex border-top border-1 position-relative pt-3 align-items-center justify-content-around"><div id="remain-seats-cards" class=" w-50 d-flex align-items-center p-1 px-lg-4 px-md-1"><img id="list-seats-left-image" src="/images/Seats-left.png" alt=""><div class="d-flex flex-column p-2 "><p id="count" class="m-0 p-0 fs-5">'
//            //        + item.availableSeats + '</p><p id="list-seats-left-text" class="m-0 p-0">Seats left</p></div></div><div id="list-deadline-info-card" class=" w-40 d-flex align-items-center p-1 px-lg-4 px-md-1"><img id="list-deadline-image" src="/images/deadline.png" alt=""><div class="d-flex flex-column p-2 "><p id="list-deadline-date" class="m-0 p-0 fs-5">'
//            //        + (item.endDate === null || item.endDate === undefined) ? '' : item.endDate.split("T")[0]  + '</p><p id="list-deadline-text" class="m-0 p-0">Deadline</p></div></div></div></div></div></div></div>';
//            //})
            
//            /*$("#list-switch").html(a);*/
////            $('#GridView').html(data);    
////        },
////        error: function (xhr, status, error) {
////            // Handle error
////            console.log(error);
////        }
////    });
////});


//---Convet-Grid-List---
localStorage.setItem("lastVisible", "grid");
var windowWidth = $(window).width();
// Check if the window width is less than 1440px
if (windowWidth < 1400) {
    // Hide the list view and show the gird view
    $('#mission-list').hide();
    $('.grid-view').show();
}
$(window).resize(function (e) {
    // Get the new window width
    var newWindowWidth = $(window).width();

    // Check if the window width is less than 1440px
    if (newWindowWidth < 1400) {
        // Hide the list view and show the gird view
        $('#mission-list').hide();
        $('.grid-view').show();
        $(".switch-view").hide();
        $("#select-list-style").hide();
    }
    else {
        $(".switch-view").show();
        $("#select-list-style").show();
        // Show the list view and hide the gird view
        if (localStorage.getItem("lastVisible") == "list") {
            $('.grid-view').hide();
            $('#mission-list').show();
        }
    }
});

let i = 0;
function isStringNullOrEmpty(s) {
    i++;
    console.warn(s === null || s === undefined)
    if (s) {
        //console.warn("valid string"+i)
        return false;
    }

    else {
        //console.warn("invalid string"+i)
        return true;
    }
        
}