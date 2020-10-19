<template>
  <el-card class="box-card">
    <div slot="header" class="clearfix">
      <span>Login</span>
    </div>
    <el-form
      :model="form"
      status-icon
      :rules="rules"
      ref="ruleForm"
      label-width="120px"
      class="demo-ruleForm"
    >
      <el-form-item label="Email" prop="email">
        <el-input
          type="text"
          v-model="form.email"
          autocomplete="off"
        ></el-input>
      </el-form-item>
      <el-form-item label="Senha" prop="password">
        <el-input
          type="password"
          v-model="form.password"
          autocomplete="off"
        ></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="submitForm('ruleForm')"
          >Logar</el-button
        >
      </el-form-item>
    </el-form>
  </el-card>
</template>

<script>
import * as service from "@/services/authenticate.service";
import { mapActions } from "vuex";

export default {
  data() {
    return {
      form: {
        email: "",
        password: "",
      },
      rules: {
        email: [
          { required: true, message: "Email é obrigatório", trigger: "blur" },
        ],
        password: [
          { required: true, message: "Senha é obrigatória", trigger: "blur" },
        ],
      },
    };
  },
  methods: {
    ...mapActions({
        "setUser": "setUser",
        "setToken": "setToken"
    }),
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.authenticate();
        }
      });
    },
    authenticate() {
      const loading = this.$loading({
        lock: true,
        text: "Loading",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });

      service.authenticate(this.form).then(
        (result) => {
          console.log(result);
          this.setUser(result.user);
          this.setToken(result.token)
          this.$router.push("home")
          loading.close();
        },
        (err) => {
          this.$swal({ icon: "error", text: err });
          loading.close();
        }
      );
    },
  },
};
</script>