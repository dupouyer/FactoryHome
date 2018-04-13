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

4-7

昨日问题全部解决完毕

鼠标点击选中物体时的逻辑判断，目前会被 collider 挡住
材料块穿透建筑的问题
建筑的 UI 界面添加
机械爪的动画优化
实现 BOX
材料块叠放处理

4-8 

鼠标穿透问题解决 （设置射线的 layermask）
材料块穿透问题解决（被抓取状态时关闭碰撞）

机械爪抓取 Box 的实现需要提前，目前生产出来的材料会被碰撞击飞

材料块叠放问题想法：
机械爪添加碰撞盒子，松爪时检查碰撞
1 实体，检查能否放入
2 材料块 位置被占用，等待下一个位置
3 空 可以放置

4-9 
机械爪从 box 中抓取物体完成（通过 HitBox 区分碰撞物类型实现）


建筑 UI
建筑状态展示
生产建筑的蓝图选择

4-10
建筑 UI 完成

处理炉子冶炼蓝图自动选择功能

4-11
炉子冶炼问题解决

传送带点击穿透问题
传送带效果优化

4-12
传送带点击穿透问题解决
传送带传送效果使用动画移动物体完成

处理传送带拥堵停滞问题