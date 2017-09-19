<template>
    <div>
        <Form ref="client" :model="client.customerEditDto" :rules="ruleValidate" :label-width="100">
            <Row>
                <Col :md="8">
                <FormItem label="客户账户" prop="account">
                    <Input v-model="client.customerEditDto.account" placeholder="客户账户"></Input>
                </FormItem>
                </Col>

                <Col :md="8">
                <FormItem label="密码" prop="password">
                    <Input type="password" v-model="client.customerEditDto.password" placeholder="密码"></Input>
                </FormItem>
                </Col>
                <Col :md="8">
                <FormItem label="推广专员" prop="displayName">
                    <Select v-model="client.customerEditDto.promoterId" style="width:200px" clearable filterable>
                        <Option :key="index" v-for="(pro,index) in promoters" :value="pro.id" :label="pro.promoterName">
                            <span>{{pro.promoterName}}</span>
                            <span style="float:right;color:#ccc">{{pro.mobile}}</span>
                        </Option>

                    </Select>
                </FormItem>
                </Col>
            </Row>
            <Row>
                <Col :md="8">
                <FormItem label="昵称" prop="customerName">
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
                                <InputNumber v-model="params.rmb" style="width:200px;" placeholder="请输入金额"></InputNumber>
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
                    promoterId: null,
                }
            }
            ,
            promoters: [],
            ruleValidate: {
                account: [
                    { required: true, message: '账户不能为空', trigger: 'blur' }
                ],
                password: [
                    { required: true, message: '密码不能为空', trigger: 'blur' }
                ],
                customerName: [
                    { required: true, message: '昵称不能为空', trigger: 'blur' }
                ]
            },
            cols: [
                {
                    type: 'index',
                    align: 'center',
                    width: '70px'
                },
                {
                    title: '充值卡卡号',
                    key: 'cardCode'
                },
                {
                    title: '金额',
                    key: 'money'
                }
            ],
            searchApi: getCards,
            params: { rmb: null, state: false },
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
                    this.client.customerEditDto = c.data.result.customer;
                    this.client.customerEditDto.gender = this.client.customerEditDto.gender != 0;
                    this.promoters = c.data.result.promoters;
                }
            })
        },

        commit() {
            this.$refs.client.validate((valid) => {
                if (valid) {
                    debugger;
                    let card = this.$refs.list.current;
                    if (card) {
                        this.client.customerEditDto.card = card.cardCode;
                    }
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
