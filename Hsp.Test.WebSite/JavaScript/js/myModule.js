// myModule.js
//export function myModule(someArg) {
//    return someArg;
//}

//模块js
export let myModule1 = {
    first_name: 'www.',
    second_name: 'baidu.com',
    getFullName: function () {
        return this.first_name + this.second_name;
    }
}

function sayHello(value = "Tony") {
    return `Hello, ${value}`;
}
export { sayHello };


////调用js
//console.log(myModule.getFullName());
//myModule.first_name = 'img.';
//console.log(myModule.getFullName());

//模块js
let _moduleName = 'module';
function setModuleName(name) {
    _moduleName = name;
}
function getModuleName() {
    return _moduleName;
}
export { setModuleName, getModuleName };












