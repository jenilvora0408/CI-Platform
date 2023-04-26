// Remove chips 
function removeChip(btn) {
    $(btn).parent().removeAttr("data-id").hide();
    $(".close-chips").hide();
    loadCard();
    loadList();
}

$(".close-chips").on("click", function (e) {
    $(".home-chips .chip").remove();
    $(this).hide();
    selectedFilters = [];
    loadCard();
    loadList();
});

// Create Chips
function showChip() {
    $(".selection .dropdown-menu li a").on("click", function (e) {
        var filtertype = $(this).attr("data-filter-type");
        var dataID = $(this).parent().val();
        // Doesn't allow same chip to create multiple times
        var existingChip = $(".home-chips .chips")
            .find(
                '[data-filter-type="' + filtertype + '"][data-id="' + dataID + '"]'
            )
            .first();
        if (existingChip.length) {
            existingChip.remove();
        }
        $(".home-chips .chips").append(
            '<div data-filter-type="' +
            filtertype +
            '" data-id="' +
            dataID +
            '" class="chip">' +
            $(this).text() +
            '<span class="closebtn" onclick="this.parentElement.style.display=\'none\'">&times;</span></div>'
        );
        $(".close-chips").show();

        $(".closebtn").on("click", function (e) {
            loadCard();
            loadList();
        });
    });

    // Fill city dropdown based on country selected
    $(".countryItem").on("click", function (e) {
        var countryId = $(this).val();
        getCities(countryId, function (data) {
            var ddlCity = $("#mission-cities");
            ddlCity.empty();
            $.each(data, function (i, city) {
                var cityItem =
                    '<li class="cityItem" value="' +
                    city.cityId +
                    '"><a data-filter-type="city" class="dropdown-item" href="#">' +
                    city.name +
                    "</a></li>";
                ddlCity.append(cityItem);
            });

            $(".cityItem").on("click", function (e) {
                loadCard();
                loadList();
            });

            showChip();
            loadCard();
            loadList();
        });
    });

    $(".cityItem").on("click", function (e) {
        showChip();
        loadCard();
        loadList();
    });
}


// Switch between grid & list view
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


$(document).ready(function () {
    /*AJAX Call for Countries - Dropdown*/

    var ddlCountry = $('#mission-countries');
    ddlCountry.append($("<ul></ul>").val(''));
    var ajaxRequest1 = $.ajax({
        url: 'https://localhost:7165/Mission/listCountries',
        type: 'GET',
        dataType: 'json',
        success: function (d) {
            var a = "";
            $.each(d, function (i, country) {
                a += '<li class="countryItem" value="' + country.countryId + '"><a data-filter-type="country" class= "dropdown-item" href = "#" >' + country.name + '</a></li>';
            });
            ddlCountry.append(a);
            $(".countryItem").on("click", function (e) {
                loadCard();
                loadList();
            })
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
                b += '<li class="cityItem" value="' + city.cityId + '"><a data-filter-type="city" class= "dropdown-item" href = "#" >' + city.name + '</a></li>';
            });
            ddlCity.append(b);
            $(".cityItem").on("click", function (e) {
                loadCard();
                loadList();
            })
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
                /*console.log(theme.missionThemeId);*/
                c += '<li class="themeItem" value="' + theme.missionThemeId + '"><a data-filter-type="theme" class= "dropdown-item" href = "#" >' + theme.title + '</a></li>';
            });
            ddltheme.append(c);
            $(".themeItem").on("click", function (e) {
                loadCard();
                loadList();
            })
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
                skills += '<li class="skillItem" value="' + skill.skillId + '"><a data-filter-type="skill" class= "dropdown-item" href = "#" >' + skill.skillName + '</a></li>';
            });
            ddlSkill.append(skills);
            $(".skillItem").on("click", function (e) {
                loadCard();
                loadList();
            })
        },
        error: function () {
            alert('Error!');
        }
    });

    $.when(ajaxRequest1, ajaxRequest2, ajaxRequest3, ajaxRequest4).done(function () {
        showChip();
    });
});


// Covert List View to Grid View if width of window is less than 1400 px
localStorage.setItem("lastVisible", "grid");
var windowWidth = $(window).width();
if (windowWidth < 1400) {
    $('#mission-list').hide();
    $('.grid-view').show();
}
$(window).resize(function (e) {
    var newWindowWidth = $(window).width();
    if (newWindowWidth < 1400) {
        $('#mission-list').hide();
        $('.grid-view').show();
        $(".switch-view").hide();
        $("#select-list-style").hide();
    }
    else {
        $(".switch-view").show();
        $("#select-list-style").show();
        if (localStorage.getItem("lastVisible") == "list") {
            $('.grid-view').hide();
            $('#mission-list').show();
        }
    }
});

