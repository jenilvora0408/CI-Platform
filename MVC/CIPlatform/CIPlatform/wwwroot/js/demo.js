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


$(".listmain-2").hide();
$("#select-list-style").on("click", function (e) {
    $(".grid-view").hide();
    $(".listmain-2").show();
});

$("#select-grid-style").on("click", function (e) {
    $(".listmain-2").hide();
    $(".grid-view").show();
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

    /*AJAX Call for Grid View*/

    $.ajax({
        type: "GET",
        url: 'https://localhost:7165/Mission/GridSP',
        dataType: "json",
        success: function (data) {
            /*Loop through the data and append each item to the grid*/
            var html = '';
            /*console.log(data);*/
            $.each(data, function (i, item) {
                /*console.log(item.countryName);*/
                
                 html += '<div class="grid-item col-sm-12 col-md-6 col-lg-4 mb-4" id="grid-column">'+
                    '<div class="card">' +
                    '<div class="card-image">' +
                    '<img class="card-img-top" src="' + item.missionImage + '" alt="Card image cap" />' +
                    '<div class="location">' +
                    '<img class="country-pin" src="/images/pin.png" alt="" />' +
                    '<span class="country">' + item.cityName + '</span>' +
                    '</div>' +
                    '<div class="feature">' +
                    '<img src="/images/heart.png" alt="" />' +
                    '<img src="/images/user.png" alt="" />' +
                    '</div>' +
                    '<p class="filter"><span>' + item.themeName + '</span></p>' +
                    '</div>' +
                    '<div class="card-body">' +
                    '<h5 class="card-title">' + item.missionTitle + '</h5>' +
                    '<div class="text-1">' +
                    '<p class="card-text">' + item.missionShortDesc + '</p>' +
                    '</div>' +
                    '<div class="ratings">' +
                    '<span>' + item.organizationName + '</span>' +
                    '<div class="rating">';
                for (var j = 0; j < 5; j++) {
                    html += '<i class="bi bi-star"></i>';
                }
                html += '</div>' +
                    '</div>' +
                    '</div>' +
                    '<hr />' +
                    '<p class="mission-date">' +
                    '<span class="time-span">From ' + Date.parse(item.cityName) + ' until ' + item.endDate + '</span>' +
                    '</p>' +
                    '<div class="card-body insight">' +
                    '<div class="card-body-left">' +
                    '<div class="card-body-left-img">' +
                    '<img src="/images/Already-volunteered.png" alt="" />' +
                    '</div>' +
                    '<div class="card-body-left-text">' +
                    '<span class="figure">' + item.availableSeats + '</span>' +
                    '<span class="seats-left">Seats left</span>' +
                    '</div>' +
                    '</div>' +
                    '<div class="card-body-right">' +
                    '<div class="card-body-right-img">' +
                    '<img src="/images/deadline.png" alt="" />' +
                    '</div>' +
                    '<div class="card-body-right-text">' +
                    '<span class="figure">' + item.endDate + '</span>' +
                    '<span class="deadline">Deadline</span>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '<hr />' +
                    '<div class="apply-btn">' +
                    '<button>' +
                    'Apply <img src="/images/right-arrow.png" alt="" />' +
                    '</button>' +
                    '<br /><br />' +
                    '</div>' +
                    '</div>' + '</div>' ;
                
            });
            $('#GridView').append(html);
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    });

    /*AJAX Call for Grid View*/
});
