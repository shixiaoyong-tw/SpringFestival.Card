# SpringFestival.Card

### 作业要求
业务场景，业务流程可以分为四个环节
1. 春晚节目单确定：实现一个春晚节目单的增删改查，节目单确定后集合进入下一个环节；
2. 手机用户投票：允许匿名用户（通过手机号码）访问并可以对节目进行投票，每位用户最多可以投三票，投票完毕后进入显示投票结果；
3. 显示投票结果：计算出投票结果，统计每个节目对应的支持率，投票完毕后进入手机抽奖环节；
4. 手机抽奖：随机返回参与手机用户投票的三位用户，作为抽奖结果的一二三等奖；要求这三位用户所参与的投票必须命中投票结果中的前三名。

实现要求：
1. 一个BFF的webapi服务，一个Business Service webapi服务, 一个local dynamoDb的数据库服务, 3个docker instance
2. 调用链：BFF -> Biz Service -> DynamoDb
3. 不要求实现UI，过程通过发送HTTP请求完成所有业务
4. 定义RESTful的webapi风格
5. 要求对请求信息进行验证

### database service
- file name: docker-compose.yml
- command: docker-compose up

### create table script
aws dynamodb create-table \
    --table-name Card \
    --attribute-definitions \
        AttributeName=Id,AttributeType=S \
    --key-schema \
        AttributeName=Id,KeyType=HASH \
    --provisioned-throughput \
        ReadCapacityUnits=5,WriteCapacityUnits=5 \
    --endpoint-url http://localhost:9000

aws dynamodb create-table \
    --table-name Audience \
    --attribute-definitions \
        AttributeName=Id,AttributeType=S \
    --key-schema \
        AttributeName=Id,KeyType=HASH \
    --provisioned-throughput \
        ReadCapacityUnits=5,WriteCapacityUnits=5 \
    --endpoint-url http://localhost:9000
