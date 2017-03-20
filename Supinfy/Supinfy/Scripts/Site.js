function counter(trackId) {
    $.ajax({
        type: 'POST',
        url: '/Music/Counter/',
        async: true,
        data: 'id=' + trackId
    });
}
$(".music").on("click", function() {
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

