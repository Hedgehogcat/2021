docker安装redis，并用配置启动
1、拉取redis镜像

docker pull redis
2、创建redis本地配置文件

　　①、去redis官网下载redis，获取redis.conf文件

　　②、修改redis.conf文件相关配置，主要修改如下：



daemonize no#用守护线程的方式启动

bind 192.168.1.1 # 注释掉这部分，使redis可以外部访问

requirepass yourpassword#给redis设置密码

appendonly yes#redis持久化

tcp-keepalive 5 #防止出现远程主机强迫关闭了一个现有的连接的错误
　
　③、使用xftp登录服务器，在/root/redis/data 创建好文件夹用于存放redis数据，这个文件夹位置可自己设定。然后将配置好的redis.conf文件复制入/root/redis/文件夹下。

3、docker启动redis

docker run -p 6379:6379 --name redis -v /root/redis/redis.conf:/etc/redis/redis.conf  -v /root/redis/data:/data -d redis:latest redis-server /etc/redis/redis.conf --appendonly yes
启动命令解释如下：

-p 6379:6379:把容器内的6379端口映射到宿主机6379端口

-v /root/redis/redis.conf:/etc/redis/redis.conf：把宿主机配置好的redis.conf放到容器内的这个位置中

-v /root/redis/data:/data：把redis持久化的数据在宿主机内显示，做数据备份

redis-server /etc/redis/redis.conf：这个是关键配置，让redis不是无配置启动，而是按照这个redis.conf的配置启动

–appendonly yes：redis启动后数据持久化

4、启动后如果出现一些警告，可参考https://www.cnblogs.com/xsjzhao/p/10882870.html进行解决

 