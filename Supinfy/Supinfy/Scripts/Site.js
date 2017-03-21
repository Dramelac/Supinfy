function counter(trackId) {
    $.ajax({
        type: 'POST',
        url: '/Music/Counter/',
        async: true,
        data: 'id=' + trackId
    });
}
$(".music").on("click", function (e) {
    if ($(e.target).hasClass("toPlaylist"))
    {
        return;
    }
    var container = document.getElementById("player_container");
    var player = document.getElementById("player");
    var img = document.getElementById("artwork");
    var music_name_container = document.getElementById("player_music_name");
    img.setAttribute("src", this.dataset.artwork);
    var artist = this.dataset.artist;
    var title = this.dataset.title;
    music_name_container.innerHTML = artist + " - " + title;
    player.setAttribute("src", this.dataset.url)
    container.style.display = "block";
    player.play();
    counter(this.dataset.id)
});




$(".toPlaylist").on("click", function (e) {
    var playlist;
    var music = $(this).closest(".music").attr("data-id");
    e.preventDefault();
    $.ajax({
        url:  ("/Playlist/Ajax_ListPlaylist"),
        type: 'POST',
        async: true,
        success: function (result) {
            playlist = result.playlists;

            var modalContent = document.getElementsByClassName("modal-body");
            while (modalContent.firstChild) {
                modalContent.removeChild(modalContent.firstChild);
            }
            for (var i = 0; i < playlist.length; i++) {
                var elem = document.createElement("form");
                elem.className = "addToPlaylist";

                var but = document.createElement("input");
                but.type = "submit";
                but.value = playlist[i].Name;
                but.className = "btn btn-primary";
                elem.appendChild(but);

                var Pid = document.createElement("input");
                Pid.type = "hidden";
                Pid.name = "playlistId";
                Pid.className = "playlistId";
                Pid.value = playlist[i].Id;
                elem.appendChild(Pid);

                var mus = document.createElement("input");
                mus.type = "hidden";
                mus.name = "trackId";
                mus.className = "trackId";
                mus.value = music;
                elem.appendChild(mus);

                modalContent[0].appendChild(elem);
                $(".addToPlaylist").submit(function (e) {
                    e.preventDefault();
                    console.log("On y est frere");
                    $.ajax({
                        url: ("/Playlist/Ajax_AddMusic"),
                        type: 'POST',
                        data: $(this).serialize(),

                        success: function (data) {
                            alert(decodeURIComponent(("Added to the Playlist")));
                        }

                    });

                });



            }
        }

    });
    });

