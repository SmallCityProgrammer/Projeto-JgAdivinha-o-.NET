async function checkGuess(event) {
  event.preventDefault();
  const userInput = document.getElementById("userInput").value;
  const response = await fetch(
    `http://localhost:5012/api/GuessingGame/${userInput}`,
    {
      method: "GET",
    }
  );
  const data = await response.json();
  const gameMessage = document.getElementById("gameMessage");

  if (response.ok) {
    gameMessage.textContent = data;
  } else {
    gameMessage.textContent = "Error: " + data.message;
  }
}

const generateNewNumber = async () => {
  try {
    const response = await fetch(
      "http://localhost:5012/api/GuessingGame/start",
      {
        method: "GET",
      }
    );

    if (response.ok) {
      const data = await response.json();
      const generatedNumber = data.GeneratedNumber;
      console.log(generatedNumber);

      displayHistory(`New number generated: ${generatedNumber}`);
    } else {
      console.error("Error:", data);
    }
  } catch (error) {
    console.error("Error:", error);
  }
};
