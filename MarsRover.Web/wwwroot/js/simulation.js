let canvas, ctx;
const colors = ['red', 'blue', 'green', 'yellow', 'purple'];

document.addEventListener('DOMContentLoaded', () => {
    canvas = document.getElementById('simulationCanvas');
    ctx = canvas.getContext('2d');

    document.getElementById('addRover').addEventListener('click', addRoverInputs);
    document.getElementById('simulationForm').addEventListener('submit', runSimulation);
});


function addRoverInputs() {
    const roverCommands = document.getElementById('roverCommands');
    const roverCount = roverCommands.children.length + 1;

    const newRover = document.createElement('div');
    newRover.className = 'rover-command';
    newRover.innerHTML = `
        <h3>Rover ${roverCount}</h3>
        <div class="form-group">
            <label for="initialPosition${roverCount}">Initial Position:</label>
            <input type="text" class="form-control initial-position" id="initialPosition${roverCount}" placeholder="e.g., 1 2 N" required>
        </div>
        <div class="form-group">
            <label for="commands${roverCount}">Commands:</label>
            <input type="text" class="form-control commands" id="commands${roverCount}" placeholder="e.g., LMLMLMLMM" required>
        </div>
    `;

    roverCommands.appendChild(newRover);
}

function runSimulation(event) {
    event.preventDefault();

    const gridSize = document.getElementById('gridSize').value;
    const roverCommands = Array.from(document.getElementsByClassName('rover-command')).map(rc => ({
        initialPosition: rc.querySelector('.initial-position').value,
        commands: rc.querySelector('.commands').value
    }));

    const data = {
        gridSize: gridSize,
        roverCommands: roverCommands
    };

    fetch('/Simulation/RunSimulation', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(result => {
            if (result.success) {
                if (Array.isArray(result.results) && result.results.length > 0) {
                    animateSimulation(gridSize, result.results);
                } else {
                    console.error('Invalid results structure:', result.results);
                }
            } else {
                console.error('Simulation error:', result.error);
                console.error('Stack trace:', result.stackTrace);
                alert('Simulation error: ' + result.error);
            }
        })
        .catch(error => {
            console.error('Network or parsing error:', error);
            alert('Error: ' + error.message);
        });
}

function animateSimulation(gridSize, results) {
    const [maxX, maxY] = gridSize.split(' ').map(Number);
    const cellSize = Math.min(canvas.width / (maxX + 1), canvas.height / (maxY + 1));

    ctx.clearRect(0, 0, canvas.width, canvas.height);
    drawGrid(maxX, maxY, cellSize);

    let roverIndex = 1; // Start from 1 to skip the grid setup result
    let stepIndex = 0;

    function animateRover() {
        if (roverIndex < results.length) {
            const roverSteps = results[roverIndex];
            if (stepIndex < roverSteps.length) {
                ctx.clearRect(0, 0, canvas.width, canvas.height);
                drawGrid(maxX, maxY, cellSize);

                // Draw all previous rovers in their final positions
                for (let i = 1; i < roverIndex; i++) {
                    const lastStep = results[i][results[i].length - 1];
                    const [x, y, facing] = lastStep.split(' ');
                    drawRover(Number(x), Number(y), facing, cellSize, colors[(i - 1) % colors.length], maxY, true);
                }

                // Draw current rover
                const [x, y, facing] = roverSteps[stepIndex].split(' ');
                drawRover(Number(x), Number(y), facing, cellSize, colors[(roverIndex - 1) % colors.length], maxY, stepIndex === roverSteps.length - 1);

                stepIndex++;
                setTimeout(animateRover, 500); // Adjust this value to change animation speed
            } else {
                // Move to next rover
                roverIndex++;
                stepIndex = 0;
                setTimeout(animateRover, 1000); // Pause between rovers
            }
        }
    }

    animateRover();
}

function drawGrid(maxX, maxY, cellSize) {
    ctx.strokeStyle = '#ccc';
    ctx.lineWidth = 1;

    for (let x = 0; x <= maxX; x++) {
        ctx.beginPath();
        ctx.moveTo(x * cellSize, 0);
        ctx.lineTo(x * cellSize, maxY * cellSize);
        ctx.stroke();
    }

    for (let y = 0; y <= maxY; y++) {
        ctx.beginPath();
        ctx.moveTo(0, y * cellSize);
        ctx.lineTo(maxX * cellSize, y * cellSize);
        ctx.stroke();
    }
}

function drawRover(x, y, direction, cellSize, color, maxY, isFinal = false) {
    const centerX = (x + 0.5) * cellSize;
    const centerY = ((maxY - y) * cellSize) - (cellSize / 2);
    const size = cellSize * 0.4;

    ctx.fillStyle = color;
    ctx.beginPath();
    ctx.arc(centerX, centerY, size / 2, 0, 2 * Math.PI);
    ctx.fill();

    ctx.strokeStyle = isFinal ? 'black' : 'white';
    ctx.lineWidth = 2;
    ctx.beginPath();
    ctx.moveTo(centerX, centerY);
    switch (direction) {
        case 'N': ctx.lineTo(centerX, centerY - size / 2); break;
        case 'E': ctx.lineTo(centerX + size / 2, centerY); break;
        case 'S': ctx.lineTo(centerX, centerY + size / 2); break;
        case 'W': ctx.lineTo(centerX - size / 2, centerY); break;
    }
    ctx.stroke();
}