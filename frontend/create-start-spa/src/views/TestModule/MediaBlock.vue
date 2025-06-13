<template>
  <div v-if="media" class="media-block">
    <img v-if="media.type === 'picture'" :src="media.url" class="media-img" alt="media" />
    <iframe
      v-else-if="media.type === 'video'"
      :src="getVideoEmbedUrl(media.url)"
      class="media-video"
      frameborder="0"
      allowfullscreen
      allow="autoplay; encrypted-media"
    ></iframe>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "MediaBlock",
  props: ["questionId", "mediaId"],
  data() {
    return {
      media: null,
      played: false,
    };
  },
  async mounted() {
    // Nếu đã có mediaId prop thì fetch trực tiếp, không cần gọi API QuestionMedias nữa
    let mediaId = this.mediaId;
    if (!mediaId) {
      const res = await axios.get(`http://localhost:5067/api/QuestionMedias/by-question/${this.questionId}`);
      const arr = res.data;
      if (arr && arr.length > 0) {
        mediaId = arr[0].mediaId;
      }
    }
    if (mediaId) {
      const mediaRes = await axios.get(`http://localhost:5067/api/media/${mediaId}`);
      this.media = mediaRes.data;
    }
  },
  methods: {
    getVideoEmbedUrl(url) {
      // Nếu là YouTube link, chuyển sang embed
      const ytMatch = url.match(/(?:youtu\.be\/|youtube\.com\/(?:watch\?v=|embed\/|v\/))([\w-]{11})/);
      if (ytMatch) {
        return `https://www.youtube.com/embed/${ytMatch[1]}`;
      }
      // Nếu là link mp4 hoặc video file, trả về luôn
      return url;
    },
    onPlay(e) {
      if (this.played) {
        // Nếu đã play xong 1 lần thì pause lại
        e.target.pause();
      }
    },
    onEnded() {
      this.played = true;
    },
    onTimeUpdate(e) {
      // Không cho tua lại
      if (this.played && e.target.currentTime < e.target.duration) {
        e.target.currentTime = e.target.duration;
      }
    },
  },
};
</script>

<style scoped>
.media-block {
  margin-bottom: 1rem;
  text-align: center;
}
.media-img {
  max-width: 100%;
  max-height: 320px;
  border-radius: 10px;
  margin: 0 auto 1rem auto;
  display: block;
}
.media-video {
  max-width: 100%;
  max-height: 340px;
  border-radius: 10px;
  margin: 0 auto 1rem auto;
  display: block;
  background: #000;
}
</style>
