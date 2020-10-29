<template>
  <!-- <h1 class="red">Hello {{ name }}!</h1> -->

  <div id="hoge">
    <p>MyChat</p>
    <p v-if="dataInitializeCompleted">Initialized.</p>
    
    <input v-model="txtUserName" placeholder="fill your name.">
    <input v-model="txtMessage" placeholder="message">
    <button v-on:click="postMessage">Post</button>
    
    <br>

    <ul id="chat" v-if="dataInitializeCompleted">
        <li v-for="(item, index) in items" :key="item.userName">
            {{ index }}: {{ item.userName }} ---> {{ item.message }}
        </li>
    </ul>   
  </div>
  
</template>



<script lang="ts">
import Vue, { PropType } from "vue"
import axios, { AxiosStatic } from 'axios';

export default Vue.extend({

    data: function() {
        return {
            items: [],
            dataInitializeCompleted: false,

            txtUserName: "",
            txtMessage: "",

            messageFetcher: new MessageFetcher(),
            messageWriter: new MessagePostman()
        }
    },
    
    methods: {
        postMessage: async function(event: any) {
            await this.messageWriter.addChatMessages("hoge", this.txtUserName, this.txtMessage);
            let messages = await this.messageFetcher.getChatMessages();
            this.items = messages;
        },
    },

    mounted() {
        this.messageFetcher.getChatMessages()
            .then(response => {
                this.items = response;
                this.dataInitializeCompleted = true;
            })
        }
    },



)


class MessagePostman {
    async addChatMessages(chatRoomName: string, userName: string, message: string): Promise<any> {
        let parameters: string = this.makeParametersForGetUri(chatRoomName, userName, message);
        return axios.get("https://localhost:44304/api/ChatRoom/AddMessage?" + parameters);
    }
    
    private makeParametersForGetUri(chatRoomName: string, userName: string, message: string): string {
        return "chatRoomName=" + chatRoomName + "&" + "userId=" + userName + "&" + "message=" + message;
    }
}

class MessageFetcher {
    async getChatMessages(): Promise<any>  {
        let messages = axios.get("https://localhost:44304/api/ChatRoom/GetMessages?chatRoomName=hoge")
                            .then((res) => {
                                return res.data;
                            });

        return messages;
    }
}



</script>