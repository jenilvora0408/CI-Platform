
	bannerImg.onchange = evt => {
		const [file] = bannerImg.files
		if (file) {
			bannerImgPreview.src = URL.createObjectURL(file)
		}
	}
	
	$(document).ready(function () {
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
		
		$('#CountryId').change(function() {debugger
			var country = $(this).val();
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
		});
		
		
		$('.upload_images').each(function() {
			$(this).on('change', function(e) {
				imgWrap = $(this).closest('.upload__box').find('.upload__img-wrap');
				var maxLength = $(this).attr('data-max_length');
				var files = e.target.files;
				var filesArr = Array.prototype.slice.call(files);
				var iterator = 0;
				
				filesArr.forEach(function(f, index) {
					if (!f.type.match('image.(png|jpe?g)')) {
						alert('Please select only .png, .jpg, or .jpeg images.');
						return;
					}
					
					if (f.size > 2*1024*1024) {
						alert('Please select an image that is less than 2 MB.');
						return;
					}
					
					formData.append("files", f);
					
					if (imgArray.length > maxLength) {
						return false;
					} else {
						var len = 0;
						for (var i = 0; i < imgArray.length; i++) {
							if (imgArray[i] !== undefined) {
								len++;
							}
						}
						if (len > maxLength) {
							return false;
						} else {
							imgArray.push(f);
							
							var reader = new FileReader();
							reader.onload = function(e) {
								var html = "<div class='upload__img-box'><div style='background-image: url(" + e.target.result + ")' data-number='" + $(".upload__img-close").length + "' data-file='" + f.name + "' class='img-bg'><div class='upload__img-close'></div></div></div>";
								imgWrap.append(html);
								iterator++;
							}
							reader.readAsDataURL(f);
						}
					}
				});
			});
		});
		
		
		$('body').on('click', ".upload__img-close", function (e) {
			var file = $(this).parent().data("file");
			for (var i = 0; i < imgArray.length; i++) {
				if (imgArray[i].name === file) {
					imgArray.splice(i, 1);
					break;
				}
			}
			$(this).parent().parent().remove();
			
			// Update formData with remaining files
			formData = new FormData();
			imgArray.forEach(function (f) {
				formData.append("files", f);
			});
		});
	})
	
	
	$(document).ready(function () {
		$(".add-skill").on("click", function (event) {
			event.preventDefault(); // prevent default behavior
			var selectedSkill = $(".available-skills .skill-list li.selected");
			if (selectedSkill.length > 0) {
				var skillValue = selectedSkill.attr("value");
				selectedSkill.appendTo($(".selected-skills .skill-list"));
				selectedSkill.removeClass("selected");
			}
		});
		
		$(".remove-skill").on("click", function (event) {
			event.preventDefault(); // prevent default behavior
			var selectedSkill = $(".selected-skills .skill-list li.selected");
			if (selectedSkill.length > 0) {
				var skillValue = selectedSkill.attr("value");
				selectedSkill.appendTo($(".available-skills .skill-list"));
				selectedSkill.removeClass("selected");
			}
		});
		
		$("#uesrskillsRight").on("click", "li", function () {
			$(this).toggleClass("selected");
		});
		
		$("#uesrskills").on("click", "li", function () {
			$(this).toggleClass("selected");
		});
		
		
		$(".user-edit-save-btn").on("click", function () {
			event.preventDefault(); // prevent default behavior
			$("#addSkillForUser").empty();
			var selectedSkills = [];
			$(".selected-skills .skill-list li").each(function () {
				var skillId = $(this).val();
				var skillName = $(this).text().trim();
				$("<li>").text(skillName).val(skillId).appendTo($("#addSkillForUser"));
				selectedSkills.push(skillId);
			});
			var skills = selectedSkills.join(',');
			$('#userSelectedSkills').val(skills);
			$(".popup-container").hide();
		});
		
		
		$(".user-edit-add-skill-btn").on("click", function (event) {
			event.preventDefault();
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
		
		// Hide popup by default
		$(".popup-container").hide();
	});
	
	$(window).click(function (event) {
		const skillpopup = $(".popup-container");
		if (event.target == skillpopup[0]) {
			skillpopup.hide();
		}
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
