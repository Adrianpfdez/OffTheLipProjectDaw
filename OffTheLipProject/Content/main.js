$(document).ready(function () {

    if (localStorage.getItem("likes-stored") === null) localStorage.setItem("likes-stored", JSON.stringify([]))
    var items = JSON.parse(localStorage.getItem("likes-stored"));
    var postId = new URLSearchParams(window.location.search).get("DocId");
    if ($("#like")[0]) {
        if (items.indexOf(postId) !== -1) {
            $('#like').removeClass('far fa-heart fa-2x');
            $('#like').addClass('fas fa-heart fa-2x');
        } else {
            $('#like').removeClass('fas fa-heart fa-2x');
            $('#like').addClass('far fa-heart fa-2x');
        }
    }

    // Carousel interval
    $('.carousel').carousel({
        interval: 10000
    });

    // Ocultar o mostrar los comentarios
    $('#contCommentsPartial').hide();
    $('#contComments').on('click',
        function () {
            $('#contCommentsPartial').toggle();
            if (document.getElementById('contCommentsPartial').style.display == "none") {
                $('#commentsTitle').text("Show Comments");
                $('#iconComment').removeClass("icon-chevron-up");
                $('#iconComment').addClass("icon-chevron-down");
            }
            else {
                $('#commentsTitle').text("Hide Comments");
                $('#iconComment').removeClass("icon-chevron-down");
                $('#iconComment').addClass("icon-chevron-up");
            }
        }
    );
    
    // Likes
    $('#like').on('click',
        function () {
            var bdContent = localStorage.getItem("likes-stored");
            if (bdContent === null) localStorage.setItem("likes-stored", JSON.stringify([])) // De objeto js a cadena
            var items = JSON.parse(localStorage.getItem("likes-stored")); // De cadena a objeto js
            var postId = new URLSearchParams(window.location.search).get("DocId");
            if (items.indexOf(postId) === -1)
            {
                $('#like').removeClass('far fa-heart fa-2x');
                $('#like').addClass('fas fa-heart fa-2x');
                items.push(postId);
            } else {
                $('#like').removeClass('fas fa-heart fa-2x');
                $('#like').addClass('far fa-heart fa-2x');
                var idx = items.indexOf(postId);
                items.splice(idx, 1);
            }
            localStorage.setItem("likes-stored", JSON.stringify(items))

            //if ($('#like').hasClass('far fa-heart fa-2x')) {
            //    $('#like').removeClass('far fa-heart fa-2x');
            //    $('#like').addClass('fas fa-heart fa-2x');
            //}
            //else {
            //    $('#like').removeClass('fas fa-heart fa-2x');
            //    $('#like').addClass('far fa-heart fa-2x');
            //}
        }
    );

    // Caja de texto buscador 
    $('#searchString').val('');
});