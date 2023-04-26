function getFavouriteMissions() {
            var userId = $(".home-nav1-right .user-btn")[0].id;
            console.log("In get fav");
            $.ajax({
                type: "GET",
                url: '/Mission/getFavouriteMissionsOfUser',
                data: { userid: userId },
                success: function (data) {
                    var dataArr = data["data"].split(",");
                console.log(dataArr);
                setTimeout(() => {
                    $("#GridView .favourite-mission-div").each(function (index) {
                        var id = this.id.slice(18);

                        console.log("In get fav:" + this.id);
                        var temp = false;
                        Loop1:

                        for (var i = 0; i < dataArr.length; i++) {
                            if (dataArr[i] == id) {
                                temp = true;

                                break Loop1;
                            }
                        }
                        if (temp == true) {
                            console.log("test");
                            this.style.backgroundColor = "red";
                            this.style.opacity = 1;
                            //console.log("True", dataArr, id, dataArr[i] == id);
                        }
                        else {
                            //console.log("False", dataArr, id, dataArr[i] == id);

                            this.style.backgroundColor = "black";
                            this.style.opacity = 0.4;
                        }
                    });
                
                }, 250);
                    
                error: function (xhr, status, error) {
                    console.log(error);
                }
            });
        }