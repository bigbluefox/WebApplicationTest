<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mixin.aspx.cs" Inherits="WebApplicationTest.Mixin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 上述3个meta标签*必须*放在最前面，任何其他内容都*必须*跟随其后！ -->
    <title>Mixin 模式</title>
    
    <script type="text/javascript">
        
        //Mixin为mix in，混入的意思

        //和多重继承类似（其实可以把 Mixin 看作多重继承的一种在特定场景下的应用），但通常混入 Mixin 的类和 Mixin 类本身不是 is-a 的关系，
        //混入 Mixin 类是为了添加某些（可选的）功能。自由地混入 Mixin 类就可以灵活地为被混入的类添加不同的功能。

        //传统的「接口」概念中并不包含实现，而 Mixin 包含实现。实际上 Mixin 的作用和 Java 中的众多以「able」结尾的接口很相似。
        //不同的是 Mixin 提供了（默认）实现，而 Java 中实现了 -able 接口的类需要类自身来实现这些混入的功能（Serializable 接口是个例外）。
        

        //如楼上很多答主一样，谈到Mixin就不得不谈到多重继承，因为Mixin的出现就是为了解决多重继承的问题，那么多重继承有什么问题呢？

        //在《松本行弘的程序世界》一书中，作者列举了以下三点：

        //结构复杂化：如果是单一继承，一个类的父类是什么，父类的父类是什么，都很明确，因为只有单一的继承关系，然而如果是多重继承的话，一个类有多个父类，这些父类又有自己的父类，那么类之间的关系就很复杂了。
        //优先顺序模糊：假如我有A，C类同时继承了基类，B类继承了A类，然后D类又同时继承了B和C类，所以D类继承父类的方法的顺序应该是D、B、A、C还是D、B、C、A，或者是其他的顺序，很不明确。
        //功能冲突：因为多重继承有多个父类，所以当不同的父类中有相同的方法是就会产生冲突。如果B类和C类同时又有相同的方法时，D继承的是哪个方法就不明确了，因为存在两种可能性。
        //当然你可以说有些语言解决了这个问题，但是并不是所有语言都想要去纠结这个问题。

        //所以为能够利用多继承的优点又解决多继承的问题，提出了规格继承和实现继承这两样东西。

        //简单来讲，规格继承指的是一堆方法名的集合，而实现继承除了方法名还允许有方法的实现。

        //Java 选择了规格继承，在 Java 中叫 interface（不过Java8中已经有默认方法了），而 Ruby 选择了实现继承，也可以叫Mixin，在 Ruby 中叫 module。

        //从某种程度上来说，继承强调 I am，Mixin 强调 I can。当你 implement 了这个接口或者 include 这个 module 的时候，然后就你行你上。

        //所以这又可以扯到 duck typing 去了，不细说。要想了解具体的可以看一下《松本行弘的程序世界》这本书。



        //以 Ruby 为例，Mix-in 有效地降低多重继承复杂性（谁是你爹，哪个爹的优先级高，你的把妹方法是继承自哪个爹的等）。 Ruby中 Mix-in 的单位是 模块 （module）。

        //Mix-in 技术按一下规则来限制多重继承：
        //继承用但一继承；
        //第二个及两个以上的父类必须是 Mix-in 的抽象类。

        //Mix-in 类是具有以下特征的抽象类：
        //不能单独生成实例；
        //不能继承普通类。

        //按照以上的原则，类在层次上具有单一继承一样的树结构，同时又可以实现功能的共享（方法是：把共享的功能放在 Mix-in 类中，再把 Mix-in 类插入到树结构里）。

        //Java 用 接口 解决规格继承（类都有哪些方法）的问题，Mix-in 则解决了实现继承（类中都用了什么数据结构和什么算法）的问题。

        //逼逼了这么多，对于 Mix-in 的理解是，Mix-in 只不过是实现多重继承的一个技巧而已。

        //Mixin是一种特殊的多重继承，也就是多重继承的子集。
        //使用Mixin的好处是，同时享有单一继承的单纯性和多重继承的共有性。

        //作为Mixin类，需要满足以下条件：
        //不能单独生成实例对象，属于抽象类。
        //不能继承Mixin以外的类。
        //因为有以上限制，Mixin类通常作为功能模块使用，在需要该功能时“混入”，而且不会使类的关系变得复杂（比如，同名方法到底从哪个父类继承）。
        //Java的接口，只提供了“规格”的多重继承。Mixin类则同时提供了“规格”和“实现”的多重继承，使用上相比接口会更加简单。





        //http://wiki.jikexueyuan.com/project/javascript-design-patterns/mixin.html

        // 能够创建自身新实体的基对象
        var Person = function (firstName, lastName) {

            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = "male";

        };

        //接下来,我们将制定一个新的类(对象),它是一个现有的Person对象的子类.让我们想象我们想要加入一个不同属性用来分辨一个Person
        //和一个继承了Person"超类"属性的Superhero.由于超级英雄分享了一般人类许多共有的特征(例如:name,gender),因此这应该很有希望
        //充分展示出子类划分是如何工作的。

        // a new instance of Person can then easily be created as follows:
        var clark = new Person("Clark", "Kent");

        // Define a subclass constructor for for "Superhero":
        var SuperHero = function (firstName, lastName, powers) {

            // Invoke the superclass constructor on the new object
            // then use .call() to invoke the constructor as a method of
            // the object to be initialized.

            Person.call(this, firstName, lastName);

            // Finally, store their powers, a new array of traits not found in a normal "Person"
            this.powers = powers;
        };

        SuperHero.prototype = Object.create(Person.prototype);
        var superman = new SuperHero("Clark", "Kent", ["flight", "heat-vision"]);
        console.log(superman);

        // Outputs Person attributes as well as powers

        //Superhero构造器创建了一个自Peroson下降的对象。这种类型的对象拥有链中位于它之上的对象的属性, 
        //而且如果我们在Person对象中设置了默认的值, Superhero能够使用特定于它的对象的值覆盖任何继承的值。

        //Mixin(织入目标类)
        //在Javascript中,我们会将从Mixin继承看作是通过扩展收集功能的一种途径.我们定义的每一个新的对象都有一个原型,从其中它可以继承更多的属性.
        //原型可以从其他对象继承而来, 但是更重要的是, 能够为任意数量的对象定义属性.我们可以利用这一事实来促进功能重用。

        //Mix允许对象以最小量的复杂性从它们那里借用(或者说继承)功能.作为一种利用Javascript对象原型工作得很好的模式,它为我们提供了从不止一个Mix
        //处分享功能的相当灵活, 但比多继承有效得多得多的方式。

        //它们可以被看做是其属性和方法可以很容易的在其它大量对象原型共享的对象.想象一下我们定义了一个在一个标准对象字面量中含有实用功能的Mixin,如下所示:

        var myMixins = {

            moveUp: function () {
                console.log("move up");
            },

            moveDown: function () {
                console.log("move down");
            },

            stop: function () {
                console.log("stop! in the name of love!");
            }

        };


        //然后我们可以方便的扩展现有构造器功能的原型,使其包含这种使用一个 如下面的score.js_.extends()方法辅助器的行为:

        // A skeleton carAnimator constructor
        function carAnimator(){
            this.moveLeft = function(){
                console.log( "move left" );
            };
        }

        // A skeleton personAnimator constructor
        function personAnimator(){
            this.moveRandomly = function(){ /*..*/ };
        }

        // Extend both constructors with our Mixin
        //_.extend( carAnimator.prototype, myMixins );
        //_.extend( personAnimator.prototype, myMixins );

        //// Create a new instance of carAnimator
        //var myAnimator = new carAnimator();
        //myAnimator.moveLeft();
        //myAnimator.moveDown();
        //myAnimator.stop();

        // Outputs:
        // move left
        // move down
        // stop! in the name of love!


        // Define a simple Car constructor
        var Car = function (settings) {

            this.model = settings.model || "no model provided";
            this.color = settings.color || "no colour provided";

        };

        // Mixin
        var Mixin = function () { };

        Mixin.prototype = {

            driveForward: function () {
                console.log("drive forward");
            },

            driveBackward: function () {
                console.log("drive backward");
            },

            driveSideways: function () {
                console.log("drive sideways");
            }

        };

        // Extend an existing object with a method from another
        function augment(receivingClass, givingClass) {

            // only provide certain methods
            if (arguments[2]) {
                for (var i = 2, len = arguments.length; i < len; i++) {
                    receivingClass.prototype[arguments[i]] = givingClass.prototype[arguments[i]];
                }
            }
            // provide all methods
            else {
                for (var methodName in givingClass.prototype) {

                    // check to make sure the receiving class doesn't
                    // have a method of the same name as the one currently
                    // being processed
                    if (!Object.hasOwnProperty(receivingClass.prototype, methodName)) {
                        receivingClass.prototype[methodName] = givingClass.prototype[methodName];
                    }

                    // Alternatively:
                    // if ( !receivingClass.prototype[methodName] ) {
                    //  receivingClass.prototype[methodName] = givingClass.prototype[methodName];
                    // }
                }
            }
        }

        // Augment the Car constructor to include "driveForward" and "driveBackward"
        augment(Car, Mixin, "driveForward", "driveBackward");

        // Create a new Car
        var myCar = new Car({
            model: "Ford Escort",
            color: "blue"
        });

        // Test to make sure we now have access to the methods
        myCar.driveForward();
        myCar.driveBackward();

        // Outputs:
        // drive forward
        // drive backward

        // We can also augment Car to include all functions from our mixin
        // by not explicitly listing a selection of them
        augment(Car, Mixin);

        var mySportsCar = new Car({
            model: "Porsche",
            color: "red"
        });

        mySportsCar.driveSideways();

        // Outputs:
        // drive sideways


        //优点 & 缺点
        //Mixin支持在一个系统中降解功能的重复性, 增加功能的重用性.在一些应用程序也许需要在所有的对象实体共享行为的地方, 我们能够通过在一个Mixin中维护这个共享的功能, 来很容易的避免任何重复, 而因此专注于只实现我们系统中真正彼此不同的功能。

        //也就是说, 对Mixin的副作用是值得商榷的.一些开发者感觉将功能注入到对象的原型中是一个坏点子, 因为它会同时导致原型污染和一定程度上的对我们原有功能的不确定性.在大型的系统中, 很可能是有这种情况的。

        //我认为, 强大的文档对最大限度的减少对待功能中的混入源的迷惑是有帮助的, 而且对于每一种模式而言, 如果在实现过程中小心行事, 我们应该是没多大问题的。


    </script>
    
    

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
