postMessage("I\'m working before postMessage(\'hello my worker\').");

onmessage = function (event) {
    postMessage("Hi " + event.data);
};