<template>
    <milk-table ref="list" :columns="cols" :search-api="searchApi" :params="params">
        <template slot="search">
            <Form ref="params" :model="params" inline :label-width="70">
                <FormItem label="金额">
                    <InputNumber v-model="params.rmb" style="width:200px;" placeholder="请输入金额"></InputNumber>
                </FormItem>
            </Form>
        </template>
    </milk-table>
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
