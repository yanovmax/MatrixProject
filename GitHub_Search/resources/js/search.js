
(function () {

    var addFavorite = function (e) {
        e.preventDefault();

        console.log($(this));
        var objPushWrapper = {};
        var objPush = {};

        var $thisFavorite = $(this).find(".item-favorite");
        var resultClass = $thisFavorite.prevObject.toggleClass("img-favorite img-favorite-full");
        if (resultClass.hasClass('img-favorite-full'))
            objPushWrapper.FavAction = 1;
        else
            objPushWrapper.FavAction = 2;
        

        if ($(this).parent('.list-item')) {
            var item = $(this).parent('.list-item');
            objPush.id = parseInt(item.attr("id"));
            objPush.avatar_url = item.find('img').attr('src');
            objPush.name = item.find('.item-name').text();
            objPushWrapper.GitHubItem = objPush;
        }

        $.ajax({
            contentType: "application/json",
            url: "/Home/SetFavoriteSession",
            data: JSON.stringify({ GhItemWrapper: objPushWrapper }),
            method: 'POST',
            success: function (res) {
                if (res) {
                }
                else {
                    alert("Favorite not added or Server error !");
                }
            },
            error: function (a, b, c) {
                alert("Error\n" + a.responseText);
            }
        });
    }

    var getGitDataByRequest = function (e) {
        e.preventDefault();
        var query = $(this).parent('.git-search-block').find('#queryText').val();

        if (query) {

            $.ajax({
                contentType: "application/json",
                url: "/Home/GetGitHubData",
                data: { queryTxt: query },
                method: 'GET',
                success: function (res) {
                    if (res) {
                        var dataResponse = $.parseJSON(res.ResponseType);

                        var $resultBlock = $('.git-result-wrapper .git-result-block ul');
                        if ($('li', $resultBlock).length > 0)
                            $resultBlock.empty();

                        if (dataResponse == 1) {
                            $.each(res.items, function (idx, item) {
                                var _item = "<li class='list-item' id=" + item.id + ">  \
                                                <div class='left-side'>\
                                                    <img src = " + item.owner.avatar_url + " class='avatar' /> \
                                                    <span class='item-name'>" + item.name + "</span>\
                                                </div>\
                                                <div id='toggler' class='item-favorite img-favorite'></div>\
                                            </li>";

                                $resultBlock.append(_item);
                            });

                            $('.git-result-block .list-item .item-favorite').on('click', addFavorite);

                            //var InstanceTestEvent = new TestEvents();
                            //InstanceTestEvent.Init();
                        }
                        else if (dataResponse == 3) {
                            alert("No results for this query");
                        }
                        else if (dataResponse == 2) {
                            alert("Server Error !\nPlease try again");
                        }
                        else if (dataResponse == 0) {
                            alert("No response ! :(\nPlease try again later")
                        }
                    }
                    else {
                        alert("No Result or Server response error !");
                    }
                },
                error: function (a, b, c) {
                    alert("Error\n" + a.responseText);
                }
            });
        }
        else {
            alert("Please input your query!")
        }
    }

    

    //handlers
    $('.container .git-search-block .btn').on("click", getGitDataByRequest);
    $('.container .git-search-block input').on("keydown", function (e) {
        if (event.keyCode == 13) {
            e.preventDefault();
            $('.container .git-search-block .btn').click();
        }
    })
})()