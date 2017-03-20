    function musicPlayer()
    {
        var container = document.getElementById("player_container");
        container.setAttribute("hidden", false);
        var player = document.getElementById("player");
        var img = document.getElementById("artwork");
        img.getAttribute("src").replace(this.dataset.artwork);
        var src1 = document.createElement("source");
        src1.innerHTML(player.setAttribute(this.dataset.title));
        player.appendChild(src1);
        this.dataset.artist;

    }
