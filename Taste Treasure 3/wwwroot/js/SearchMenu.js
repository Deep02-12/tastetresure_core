document.getElementById('srcMenu').addEventListener('click', function () {
    var searchQuery = document.querySelector('.search').value.trim().toLowerCase();

    var recipeTitles = document.querySelectorAll('.content-title h5:first-child');

    recipeTitles.forEach(function (title) {
        var recipeName = title.textContent.trim().toLowerCase();
        var parentContainer = title.closest('.content-wrapper');

        // Check if the recipe name includes the search query
        if (recipeName.includes(searchQuery)) {
            parentContainer.style.display = 'block'; 
        } else {
            parentContainer.style.display = 'none'; 
        }
    });
});
//