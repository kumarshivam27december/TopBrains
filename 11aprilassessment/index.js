function getNumbers(){
    let a = parseFloat(document.getElementById("num1").value);
    let b = parseFloat(document.getElementById("num2").value);
    return {a,b};
}

function add(){
    let {a,b} = getNumbers();
    let result = a + b;
    document.getElementById("result").innerText = `${a} + ${b} = ${result}`;
}

function subtract(){
    let {a,b} = getNumbers();
    let result = a - b;
    document.getElementById("result").innerText = `${a} - ${b} = ${result}`;
}

function multiply(){
    let {a,b} = getNumbers();
    let result = a * b;
    document.getElementById("result").innerText = `${a} × ${b} = ${result}`;
}

function divide(){
    let {a,b} = getNumbers();

    if(b === 0){
        document.getElementById("result").innerText = "Cannot divide by zero";
        return;
    }

    let result = (a / b).toFixed(2);
    document.getElementById("result").innerText = `${a} ÷ ${b} = ${result}`;
}