    bannerImg.onchange = evt => {
        const [file] = bannerImg.files
        if (file) {
            bannerImgPreview.src = URL.createObjectURL(file)
        }
    }
    function getCityOfCountry(){
        var country = $('#CountryId').val();
            $.ajax({
                url: '@Url.Action("GetCitiesOfCountry", "Admin")',
                type: 'GET',
                data: { country: country },
                success: function(data) {
                    // Populate the city dropdown with the returned data
                    var cityDropdown = $('#CityId');
                    cityDropdown.empty();
                    $.each(data, function(index, item) {
                        cityDropdown.append('<option value="' + item.cityId + '">' + item.name + '</option>');
                    });
                }
            });
    }

    $(document).ready(function () {
        getCityOfCountry();
        $(".user-edit-add-skill-btn").on("click", function () {
            $(".popup-container").show();
        });

        $(".user-edit-close-btn").on("click", function () {
            $(".popup-container").hide();
        });

        $(".close-popup-btn").click(function () {
            $(".popup-container").hide();
        });

        $(window).click(function (event) {
            const skillpopup = $(".popup-container");
            if (event.target == skillpopup[0]) {
                skillpopup.hide();
            }
        });

        $(".add-skill").on("click", function () {
            var selectedSkill = $(".available-skills .skill-list li.selected");
            if (selectedSkill.length > 0) {
                var skillValue = selectedSkill.attr("value");
                selectedSkill.appendTo($(".selected-skills .skill-list"));
                selectedSkill.removeClass("selected");
            }
        });

        $(".remove-skill").on("click", function () {
            var selectedSkill = $(".selected-skills .skill-list li.selected");
            if (selectedSkill.length > 0) {
                var skillValue = selectedSkill.attr("value");
                selectedSkill.appendTo($(".available-skills .skill-list"));
                selectedSkill.removeClass("selected");
            }
        });

        $('#uesrskillsRight').on('click', 'li', function() {
            $(this).toggleClass('selected');
        });

        $('#uesrskills').on('click', 'li', function() {
            $(this).toggleClass('selected');
        });


        $(".user-edit-save-btn").on("click", function () {
            $(".user-edit-selected-skill").empty();
            var selectedSkills = [];
            $(".selected-skills .skill-list li").each(function () {
                var skillId = $(this).val();
                var skillName = $(this).text().trim();
                $("<li>").text(skillName).val(skillId).appendTo($(".user-edit-selected-skill"));
                selectedSkills.push(skillId);
            });
            var skills = selectedSkills.join(',');
            $('#userSelectedSkills').val(skills);
            $(".popup-container").hide();
        });

        $('#CountryId').change(function() {
            getCityOfCountry();
        });


    // Color Change - Sidebar
    $(".cms-sidebar ul li").on("mouseenter", function () {
        $(this).find("i").css("color", "#F88634");
        $(this).find("span").css("color", "#F88634");
    });
    $(".cms-sidebar ul li").on("mouseleave", function () {
        if (!$(this).hasClass("active")) {
            $(this).find("i").css("color", "#fff");
            $(this).find("span").css("color", "#fff");
        }
    });
    $(".cms-sidebar ul li").on("click", function () {
        $(".cms-sidebar ul li").removeClass("active");
        $(".cms-sidebar ul li i").css("color", "#fff");
        $(".cms-sidebar ul li span").css("color", "#fff");
        $(this).addClass("active");
        $(this).find("i").css("color", "#F88634");
        $(this).find("span").css("color", "#F88634");
    });

    // Hide & Show Sidebar
    $("#sidebar-open").hide();
    $("#sidebar-close").hide();

    $(window).resize(function () {
        if ($(window).width() < 960) {
            $(".cms-sidebar").hide();
            $("#sidebar-open").show();
            $("#sidebar-close").hide();
        } else {
            $(".cms-sidebar").show();
            $("#sidebar-open").hide();
        }
    });


    // Open & Close Button - Sidebar
    $("#sidebar-open").on("click", function (e) {
        $(".cms-sidebar").show();
        $("#sidebar-open").hide();
        $("#sidebar-close").show();
    });

    $("#sidebar-close").on("click", function (e) {
        $(".cms-sidebar").hide();
        $("#sidebar-close").hide();
        $("#sidebar-open").show();
    });
    })