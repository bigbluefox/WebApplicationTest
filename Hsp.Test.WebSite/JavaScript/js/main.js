//import { myModule } from './myModule.js';// main.js
import { name, age, title } from './test.js';// test.js
console.log(`${name},${age},${title}`);

import * as t1 from './test.js';
console.log(`${t1.name},${t1.age},${t1.title}`);//1 2 

//调用js
import { getModuleName, setModuleName } from './myModule.js';
setModuleName("es6 Module");
console.log(getModuleName());


export let ok = 1;

export let add = function (a, b) {
    return a + b;
}

export let sayHello = function (value = "Tony") {
    return `Hello, ${value}`;
}



let yes = 2;

let getName = function () {
    return 'Bob';
}

export { yes, getName };


let add1 = function (a, b) {
    return a + b
}

class Ball {
    constructor() { }
}

// 默认导出需要注意：每个模块中，只允许使用唯一的一次 export default，否则会报错！
export default {
    add1, Ball, name, age, title
}









