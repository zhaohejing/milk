<template>
    <div>
        <Form ref="client" :model="client.customerEditDto" :rules="ruleValidate" :label-width="100">
            <Row>
                <Col :md="8">
                <FormItem label="客户账户" prop="displayName">
                    <Input v-model="client.customerEditDto.account" placeholder="客户账户"></Input>
                </FormItem>
                </Col>

                <Col :md="8">
                <FormItem label="密码" prop="displayName">
                    <Input v-model="client.customerEditDto.password" placeholder="密码"></Input>
                </FormItem>
                </Col>
                <Col :md="8">
                <FormItem label="推广专员" prop="displayName">
                    <Select v-model="client.customerEditDto.promoterId" style="width:200px">
                        <Option value="beijing" label="北京市">
                            <span>北京</span>
                            <span style="float:right;color:#ccc">Beiing</span>
                        </Option>
                        <Option value="shanghai" label="上海市">
                            <span>上海</span>
                            <span style="float:right;color:#ccc">ShangHai</span>
                        </Option>
                        <Option value="shenzhen" label="深圳市">
                            <span>深圳</span>
                            <span style="float:right;color:#ccc">ShenZhen</span>
                        </Option>
                    </Select>
                </FormItem>
                </Col>
            </Row>
            <Row>
                <Col :md="8">
                <FormItem label="昵称" prop="displayName">
                    <Input v-model="client.customerEditDto.customerName" placeholder="昵称"></Input>
                </FormItem>
                </Col>
                <Col :md="8">
                <FormItem label="手机" prop="displayName">
                    <Input v-model="client.customerEditDto.mobile" placeholder="手机"></Input>
                </FormItem>
                </Col>
                <Col :md="8">
                <FormItem label="性别">
                    <i-switch v-model="client.customerEditDto.gender">
                        <span slot="open">男</span>
                        <span slot="close">女</span>
                    </i-switch>
                </FormItem>
                </Col>
            </Row>
        </Form>
        <Row>
            <Card>
                <p slot="title">绑定充值卡</p>
                <milk-table ref="list" :columns="cols" :search-api="searchApi" :params="params">
                    <template slot="search">
                        <Form ref="params" :model="params" inline :label-width="70">
                            <FormItem label="金额">
                                <Input v-model="params.rmb" placeholder="金额"></Input>
                            </FormItem>
                        </Form>
                    </template>
                </milk-table>
            </Card>
        </Row>
    </div>
</template>
<script>
import { updateClient, getClientForEdit } from 'api/client';
import { getCards } from 'api/card';

export default {
    name: 'modifyClient',
    props: {
        clientId: {
            type: Number,
            default() {
                return null
            }
        }
    },
    data() {
        return {
            client: {
                customerEditDto: {
                    id: this.clientId,
                    account: "",
                    customerName: "",
                    mobile: "",
                    gender: true,
                    password: "",
                    promoterId: 0,
                }
            },
            generalizeList: [],
            ruleValidate: {
                displayName: [
                    { required: true, message: '角色名不能为空', trigger: 'blur' }
                ]
            },
            cols: [
                {
                    type: 'selection',
                    align: 'center',
                    width: '70px'
                },
                {
                    title: '充值卡卡号',
                    key: 'displayName'
                },
                {
                    title: '金额',
                    key: 'description'
                }
            ],
            searchApi: getCards,
            params: { rmb: '' },
        }
    },
    created() {
        this.init();
    },
    mounted() {

    },
    methods: {
        async init() {
            getClientForEdit({ id: this.client.customerEditDto.id }).then(c => {
                if (c.data.success) {
                    this.client = c.data.result;
                }
            })
        },

        commit() {
            this.$refs.client.validate((valid) => {
                if (valid) {
                    updateClient(this.client).then(r => {
                        if (r.data.success) {
                            this.$root.eventHub.$emit('client');
                        } else {
                            this.$root.eventHub.$emit('client');
                        }
                    });
                } else {
                    this.$Message.error('表单验证失败!');
                    this.$root.eventHub.$emit('client');
                }
            })
        }
    }
}
</script>
