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

$(document).ready(function () {
    /*debugger*/
    var ddlCountry = $('.mission-Countrys');
    ddlCountry.append($("<ul></ul>").val(''));
    $.ajax({
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
            showChip();
            
        },
        error: function () {
            alert('Error!');
        }
    });
});

$(document).ready(function () {
    /*debugger*/
    var ddlCountry = $('.mission-Cities');
    ddlCountry.append($("<ul></ul>").val(''));
    $.ajax({
        url: 'https://localhost:7165/Mission/listCities',
        type: 'GET',
        dataType: 'json',
        success: function (d) {
            /*debugger*/
            var a = "";
            $.each(d, function (i, city) {
                a += '<li><a class= "dropdown-item" href = "#" >' + city.name + '</a></li>';
            });
            ddlCountry.append(a);
            showChip();

        },
        error: function () {
            alert('Error!');
        }
    });
});

$(document).ready(function () {
    /*debugger*/
    var ddlCountry = $('.mission-Theme');
    ddlCountry.append($("<ul></ul>").val(''));
    $.ajax({
        url: 'https://localhost:7165/Mission/listTheme',
        type: 'GET',
        dataType: 'json',
        success: function (d) {
            /*debugger*/
            var a = "";
            $.each(d, function (i, theme) {
                a += '<li><a class= "dropdown-item" href = "#" >' + theme.title + '</a></li>';
            });
            ddlCountry.append(a);
            showChip();

        },
        error: function () {
            alert('Error!');
        }
    });
});

$(document).ready(function () {
    /*debugger*/
    var ddlCountry = $('.mission-Skill');
    ddlCountry.append($("<ul></ul>").val(''));
    $.ajax({
        url: 'https://localhost:7165/Mission/listSkill',
        type: 'GET',
        dataType: 'json',
        success: function (d) {
            /*debugger*/
            var a = "";
            $.each(d, function (i, skill) {
                a += '<li><a class= "dropdown-item" href = "#" >' + skill.skillName + '</a></li>';
            });
            ddlCountry.append(a);
            showChip();

        },
        error: function () {
            alert('Error!');
        }
    });
});