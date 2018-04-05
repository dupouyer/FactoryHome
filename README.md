# README #

This README would normally document whatever steps are necessary to get your application up and running.

### What is this repository for? ###

* Quick summary
* Version
* [Learn Markdown](https://bitbucket.org/tutorials/markdowndemo)

Build a unity game for mobile phone

How

### How do I get set up? ###

* Summary of set up
* Configuration
* Dependencies
* Database configuration
* How to run tests
* Deployment instructions

### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines

### Who do I talk to? ###

* Repo owner or admin
* Other community or team contact

# Framework #

### Globals  
* 放置一些全局功能的引用

### PanelManager
* 界面管理
* 创建界面
* 显示关闭界面

### LoaderManager
* 资源加载

### ObjectManager
* 管理物件
* 创建物件
* 销毁物件

### Object
 一个物件,可通过配置实例化所属 GameObject

### ModeConfig
一个 GameObject 的配置,可通过此配置生产一个 GameObject

### Transport
传送带功能

### Arm
机械臂功能

### Factory
工厂功能，可通过 Blueprint 生产物件

### Blueprint
一个物件的蓝图

开发日志: 
3-30:
蓝图生产系统
角色控制
角色控制 UI
渲染效果

4-6
实体的计数解决方案：
实现插槽类 
Slot {id, num}
存储插槽内的实体类型和数量
移除 Enity 类，实体不再需要一个一对一的对象来存储属性和配置。
实体对配置以及行为都是多对一来处理。

抛弃 EquipBase 基类，用接口来处理多个实体显示对象的操作行为

待解决：
实体的旋转工具（增加一个旋转按键，标识当前的旋转方向，激活能旋转选择中的实体）
镜头控制
UI 的点击穿透问题