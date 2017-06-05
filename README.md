# SeApi

SeApi是一个自定义的api框架，通过实现IRouteHandler和IHttpHandler，实现自定义的路由以及http请求相应

## 如何运行

下载之后直接在vs中运行即可，本项目没有显示界面，运行看到的是403错误，不用担心，我们的api可以正常访问

## 框架提供了什么

框架提供了一个编写模式，它可以让不同开发者合理协作开发web api;我们的api工程项目放在apiimps目录下，编写方式参考tempdata,我们可以建立无数个api编写项目，只要它们的编写方式是一致的；
我们需要报所有生成的apidll放到seapi项目的bin/apis目录下，我们一般直接使用项目工程的 生成事件来做 xcopy/y "$(TargetFileName)" "$(SolutionDir)\SeApi\bin\Apis\"


## 详细介绍

1. api编写方式

    新建项目，引用common和core，然后，编写方法，继承ApiMethodHandler就行了，其他的什么也不需要关系,项目名字最后一定要为api，比如tempapi

    [SePost]    
    public class TempData : ApiMethodHandler<TempResponse, TempDataRequest>
    {

        public override TempResponse Invoke(TempDataRequest request)
        {
            var res = new TempResponse();
            var blog = new Blog();
            blog.title = request.Name;
            blog.id = 1;
            res.blog = blog;
            res.data = "这里是 data";
            return res;
        }
    }

2. 方法验证

    项目提供了签名验证和参数验证等方法，不过具体项目应该根据自己项目的需要去做这些事情;

    我们提供了编写这些的一些规范方法，参考core项目里面的checker，这些参数或者是签名的额验证方法我们建议是直接放在apibasemethodHandler这个文件里面去做，而我们的这个框架在这个地方没有过多处理一些事情，很多东西需要你自己去做
    
    我们提供的检验有 系统必要参数检验  SystemParamsChecker，特性参数检验，当类的属性打上Required，TimeInfo，StringLengh特性之后，我们回去检验参数的正确性

    api方法默认是post访问的，如果需要get访问，请使用SeGet特性，暂时只提供了get和post访问

3. common

    common提供了一些公用方法，比如一些excption，一些扩展方法

4. 关于编写完成之后

    完成之后，我们还不知道api的访问方法？

    get访问方式：localhost:***/api?method=xxx
    method=前缀+项目名称（去掉api三个字母之后）+方法名，
    比如tempapi项目的tempdata方法
    method=se.temp.tempdata
    比如我们tempdata项目，如果是get访问方式，那么get请求就应该是
    localhost:***/api?method=se.temp.tempdata?Name=xxx&Id=xxx

    如果是post方法，那么访问方式应该是
    localhost:***/api

    body：

| key  | value|
| ------------- | ------------- |
| method  | se.temp.tempdata  |
|  Name  | ***  |
|  Id  | ***  |

        这是第一种访问方式，我们还可以不传入method方法，直接使用
        localhost:***/api/se/temp/tempdata
        来对接口进行访问

    

