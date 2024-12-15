document
  .getElementById("wishForm")
  .addEventListener("submit", async function (event) {
    event.preventDefault();

    const name = document.getElementById("name").value;
    const message = document.getElementById("message").value;

    const response = await fetch("/api/wishes", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ name, message }),
    });

    if (response.ok) {
      document.getElementById("wishForm").reset();

      loadWishes();
    } else {
      alert("Whoopsie, database kapot");
    }
  });

async function loadWishes() {
  const response = await fetch("/api/wishes");
  const wishes = await response.json();
  const wishList = document.getElementById("wishList");

  wishList.innerHTML = "";

  wishes.forEach((wish) => {
    const formattedDate = new Date(wish.date).toLocaleDateString();
    wishList.innerHTML += `<li class="wish-items"> Van: <strong>${wish.name}</strong> - ${wish.message} <br> (${formattedDate})</li>`;
  });
}

const snowContainer = document.querySelector('.snow-container');
const numberOfSnowflakes = 500;

for (let i = 0; i < numberOfSnowflakes; i++) {
    const snowFlake = document.createElement('div');
    snowFlake.classList.add('snow');

    // Alle "random" eigenschappen van de sneeuwvlokjes
    const randomX = Math.random() * 98;
    const randomDelay = Math.random() * 60;
    const randomScale = 0.1 + Math.random() * 0.5;

    snowFlake.style.opacity = Math.random();
    snowFlake.style.left = `${randomX}vw`;
    snowFlake.style.transform = `translateY(-10px) scale(${randomScale})`;
    snowFlake.style.animation = `fall ${10 + Math.random() * 20}s -${randomDelay}s linear infinite`;

    // Dynamisch sneeuwvlokjes toevoegen aan de container
    snowContainer.appendChild(snowFlake);
}

loadWishes();