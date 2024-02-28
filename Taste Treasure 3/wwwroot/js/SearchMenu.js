document.addEventListener("DOMContentLoaded", function () {
    const searchButton = document.getElementById("srcMenu");

    searchButton.addEventListener("click", function () {
        const searchInput = document.querySelector(".search");
        const searchTerm = searchInput.value.toLowerCase().trim();

        if (searchTerm === "") {
            return; // Do nothing if search term is empty
        }

        const recipeTitles = document.querySelectorAll(".content-title h5:first-child");

        // Array to store matching recipe cards
        const matchingRecipes = [];

        // Loop through each recipe title
        recipeTitles.forEach(function (title) {
            const recipeTitle = title.textContent.toLowerCase();
            const parentContentWrapper = title.closest(".content-wrapper");

            if (recipeTitle.includes(searchTerm)) {
                matchingRecipes.push(parentContentWrapper);
                parentContentWrapper.remove(); // Remove matched card from its current position
            }
        });

        // Add matched recipe cards to the top of the list
        const menuContent = document.querySelector(".menu-content-pic");
        matchingRecipes.forEach(function (recipe) {
            menuContent.prepend(recipe);
        });

        // Scroll to the top of the page
        window.scrollTo({ top: 0, behavior: "smooth" });
    });
});
