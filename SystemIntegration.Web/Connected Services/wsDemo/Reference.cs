﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SystemIntegration.Web.wsDemo {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="wsDemo.WsInsertStringSoap")]
    public interface WsInsertStringSoap {
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 HelloWorldResult 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        SystemIntegration.Web.wsDemo.HelloWorldResponse HelloWorld(SystemIntegration.Web.wsDemo.HelloWorldRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<SystemIntegration.Web.wsDemo.HelloWorldResponse> HelloWorldAsync(SystemIntegration.Web.wsDemo.HelloWorldRequest request);
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 sql 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/InsertSql", ReplyAction="*")]
        SystemIntegration.Web.wsDemo.InsertSqlResponse InsertSql(SystemIntegration.Web.wsDemo.InsertSqlRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/InsertSql", ReplyAction="*")]
        System.Threading.Tasks.Task<SystemIntegration.Web.wsDemo.InsertSqlResponse> InsertSqlAsync(SystemIntegration.Web.wsDemo.InsertSqlRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld", Namespace="http://tempuri.org/", Order=0)]
        public SystemIntegration.Web.wsDemo.HelloWorldRequestBody Body;
        
        public HelloWorldRequest() {
        }
        
        public HelloWorldRequest(SystemIntegration.Web.wsDemo.HelloWorldRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class HelloWorldRequestBody {
        
        public HelloWorldRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorldResponse", Namespace="http://tempuri.org/", Order=0)]
        public SystemIntegration.Web.wsDemo.HelloWorldResponseBody Body;
        
        public HelloWorldResponse() {
        }
        
        public HelloWorldResponse(SystemIntegration.Web.wsDemo.HelloWorldResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class HelloWorldResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorldResult;
        
        public HelloWorldResponseBody() {
        }
        
        public HelloWorldResponseBody(string HelloWorldResult) {
            this.HelloWorldResult = HelloWorldResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class InsertSqlRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="InsertSql", Namespace="http://tempuri.org/", Order=0)]
        public SystemIntegration.Web.wsDemo.InsertSqlRequestBody Body;
        
        public InsertSqlRequest() {
        }
        
        public InsertSqlRequest(SystemIntegration.Web.wsDemo.InsertSqlRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class InsertSqlRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string sql;
        
        public InsertSqlRequestBody() {
        }
        
        public InsertSqlRequestBody(string sql) {
            this.sql = sql;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class InsertSqlResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="InsertSqlResponse", Namespace="http://tempuri.org/", Order=0)]
        public SystemIntegration.Web.wsDemo.InsertSqlResponseBody Body;
        
        public InsertSqlResponse() {
        }
        
        public InsertSqlResponse(SystemIntegration.Web.wsDemo.InsertSqlResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class InsertSqlResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string InsertSqlResult;
        
        public InsertSqlResponseBody() {
        }
        
        public InsertSqlResponseBody(string InsertSqlResult) {
            this.InsertSqlResult = InsertSqlResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WsInsertStringSoapChannel : SystemIntegration.Web.wsDemo.WsInsertStringSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WsInsertStringSoapClient : System.ServiceModel.ClientBase<SystemIntegration.Web.wsDemo.WsInsertStringSoap>, SystemIntegration.Web.wsDemo.WsInsertStringSoap {
        
        public WsInsertStringSoapClient() {
        }
        
        public WsInsertStringSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WsInsertStringSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsInsertStringSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsInsertStringSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SystemIntegration.Web.wsDemo.HelloWorldResponse SystemIntegration.Web.wsDemo.WsInsertStringSoap.HelloWorld(SystemIntegration.Web.wsDemo.HelloWorldRequest request) {
            return base.Channel.HelloWorld(request);
        }
        
        public string HelloWorld() {
            SystemIntegration.Web.wsDemo.HelloWorldRequest inValue = new SystemIntegration.Web.wsDemo.HelloWorldRequest();
            inValue.Body = new SystemIntegration.Web.wsDemo.HelloWorldRequestBody();
            SystemIntegration.Web.wsDemo.HelloWorldResponse retVal = ((SystemIntegration.Web.wsDemo.WsInsertStringSoap)(this)).HelloWorld(inValue);
            return retVal.Body.HelloWorldResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SystemIntegration.Web.wsDemo.HelloWorldResponse> SystemIntegration.Web.wsDemo.WsInsertStringSoap.HelloWorldAsync(SystemIntegration.Web.wsDemo.HelloWorldRequest request) {
            return base.Channel.HelloWorldAsync(request);
        }
        
        public System.Threading.Tasks.Task<SystemIntegration.Web.wsDemo.HelloWorldResponse> HelloWorldAsync() {
            SystemIntegration.Web.wsDemo.HelloWorldRequest inValue = new SystemIntegration.Web.wsDemo.HelloWorldRequest();
            inValue.Body = new SystemIntegration.Web.wsDemo.HelloWorldRequestBody();
            return ((SystemIntegration.Web.wsDemo.WsInsertStringSoap)(this)).HelloWorldAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SystemIntegration.Web.wsDemo.InsertSqlResponse SystemIntegration.Web.wsDemo.WsInsertStringSoap.InsertSql(SystemIntegration.Web.wsDemo.InsertSqlRequest request) {
            return base.Channel.InsertSql(request);
        }
        
        public string InsertSql(string sql) {
            SystemIntegration.Web.wsDemo.InsertSqlRequest inValue = new SystemIntegration.Web.wsDemo.InsertSqlRequest();
            inValue.Body = new SystemIntegration.Web.wsDemo.InsertSqlRequestBody();
            inValue.Body.sql = sql;
            SystemIntegration.Web.wsDemo.InsertSqlResponse retVal = ((SystemIntegration.Web.wsDemo.WsInsertStringSoap)(this)).InsertSql(inValue);
            return retVal.Body.InsertSqlResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SystemIntegration.Web.wsDemo.InsertSqlResponse> SystemIntegration.Web.wsDemo.WsInsertStringSoap.InsertSqlAsync(SystemIntegration.Web.wsDemo.InsertSqlRequest request) {
            return base.Channel.InsertSqlAsync(request);
        }
        
        public System.Threading.Tasks.Task<SystemIntegration.Web.wsDemo.InsertSqlResponse> InsertSqlAsync(string sql) {
            SystemIntegration.Web.wsDemo.InsertSqlRequest inValue = new SystemIntegration.Web.wsDemo.InsertSqlRequest();
            inValue.Body = new SystemIntegration.Web.wsDemo.InsertSqlRequestBody();
            inValue.Body.sql = sql;
            return ((SystemIntegration.Web.wsDemo.WsInsertStringSoap)(this)).InsertSqlAsync(inValue);
        }
    }
}
