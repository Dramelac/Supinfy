function counter(trackId) {
    $.ajax({
        type: 'POST',
        url: '/Music/Counter/',
        async: true,
        data: 'id=' + trackId
    });
}