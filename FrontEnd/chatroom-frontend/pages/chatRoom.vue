<template>
  <!-- <h1 class="red">Hello {{ name }}!</h1> -->

  <div id="hoge">
    <p>MyChat</p>
    

    <ul id="chat">
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

    props: {
        items:Array
    },

    mounted () {
        let hoge: Hoge = new Hoge();
        let itemsData: object;

        hoge.getChatMessages()
            .then(response => {
                this.items = response.data;
            })


        }
  }
})

class Hoge {
    public hoge: any;

    async getChatMessages(): Promise<any>  {
        let messages = axios.get("https://localhost:44304/api/ChatRoom/GetMessages?chatRoomName=hoge")
                            .then((res) => {
                                // this.hoge = res.data.body;
                                return res.data.body;
                            });

        return messages;

    }

}


</script>