<template>
  <div class="add-courses-page">
    <div class="main-content">
      <UserDashboard class="sidebar" />
      <div class="form-section">
        <el-steps :active="step" finish-status="success" align-center>
          <el-step title="Thêm khóa học" />
          <el-step title="Thêm bài học" />
          <el-step title="Thêm bài kiểm tra" />
          <el-step title="Thêm câu hỏi" />
        </el-steps>

        <div v-if="step === 0" class="step-form">
          <h2>Thêm khóa học mới</h2>
          <el-form :model="courseForm" label-width="120px">
            <el-form-item label="Tên khóa học">
              <el-input v-model="courseForm.title" placeholder="Nhập tên khóa học" />
            </el-form-item>
            <el-form-item label="Mô tả">
              <el-input type="textarea" v-model="courseForm.description" placeholder="Nhập mô tả" />
            </el-form-item>
            <el-form-item label="Ảnh đại diện">
              <el-upload
                class="avatar-uploader"
                :show-file-list="false"
                :before-upload="beforeAvatarUpload"
                :on-change="handleAvatarChange"
                :auto-upload="false"
              >
                <img v-if="courseForm.imageUrl" :src="courseForm.imageUrl" class="avatar" />
                <el-icon v-else><Plus /></el-icon>
              </el-upload>
            </el-form-item>
            <el-form-item label="Cấp độ">
              <el-input v-model="courseForm.level" placeholder="Nhập cấp độ (ví dụ: Cơ bản, Nâng cao...)" />
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="submitCourse">Lưu & Tiếp tục</el-button>
            </el-form-item>
          </el-form>
        </div>

        <div v-if="step === 1" class="step-form">
          <h2>Thêm bài học cho khóa học: {{ courseForm.title }}</h2>
          <el-form :model="lessonForm" label-width="120px">
            <el-form-item label="Tên bài học">
              <el-input v-model="lessonForm.title" placeholder="Nhập tên bài học" />
            </el-form-item>
            <el-form-item label="Mô tả">
              <el-input type="textarea" v-model="lessonForm.description" placeholder="Nhập mô tả" />
            </el-form-item>
            <el-form-item label="Kỹ năng">
              <el-input v-model="lessonForm.skill" placeholder="Nhập kỹ năng (ví dụ: Reading, Listening...)" />
            </el-form-item>
            <el-form-item label="Nội dung">
              <el-input type="textarea" v-model="lessonForm.content" placeholder="Nhập nội dung bài học" />
            </el-form-item>
            <el-form-item label="Thứ tự">
              <el-input v-model.number="lessonForm.order" type="number" min="1" placeholder="Thứ tự bài học" />
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="submitLesson">Lưu bài học</el-button>
              <el-button @click="nextStep">Tiếp tục</el-button>
            </el-form-item>
          </el-form>
          <el-table :data="lessons" style="margin-top: 20px">
            <el-table-column prop="title" label="Tên bài học" />
            <el-table-column prop="description" label="Mô tả" />
          </el-table>
        </div>

        <div v-if="step === 2" class="step-form">
          <h2>Thêm bài kiểm tra cho bài học</h2>
          <el-select v-model="selectedLessonId" placeholder="Chọn bài học" style="width: 100%; margin-bottom: 20px;">
            <el-option v-for="lesson in lessons" :key="lesson.id" :label="lesson.title" :value="lesson.id" />
          </el-select>
          <el-form :model="testForm" label-width="120px">
            <el-form-item label="Tên bài kiểm tra">
              <el-input v-model="testForm.title" placeholder="Nhập tên bài kiểm tra" />
            </el-form-item>
            <el-form-item label="Mô tả">
              <el-input type="textarea" v-model="testForm.description" placeholder="Nhập mô tả" />
            </el-form-item>
            <el-form-item label="Kỹ năng">
              <el-input v-model="testForm.skill" placeholder="Nhập kỹ năng (ví dụ: Reading, Listening...)" />
            </el-form-item>
            <el-form-item label="Thứ tự">
              <el-input v-model.number="testForm.order" type="number" min="1" placeholder="Thứ tự bài kiểm tra" />
            </el-form-item>
            <el-form-item label="Thời lượng (phút)">
              <el-input v-model.number="testForm.duration" type="number" min="1" placeholder="Thời lượng (phút)" />
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="submitTest">Lưu bài kiểm tra</el-button>
              <el-button @click="nextStep">Tiếp tục</el-button>
            </el-form-item>
          </el-form>
          <el-table :data="tests" style="margin-top: 20px">
            <el-table-column prop="title" label="Tên bài kiểm tra" />
            <el-table-column prop="description" label="Mô tả" />
          </el-table>
        </div>

        <div v-if="step === 3" class="step-form">
          <h2>Thêm câu hỏi cho bài kiểm tra</h2>
          <el-select v-model="selectedTestId" placeholder="Chọn bài kiểm tra" style="width: 100%; margin-bottom: 20px;">
            <el-option v-for="test in tests" :key="test.id" :label="test.title" :value="test.id" />
          </el-select>
          <el-form :model="questionForm" label-width="120px">
            <el-form-item label="Loại câu hỏi">
              <el-select v-model="questionForm.type" placeholder="Chọn loại câu hỏi">
                <el-option v-for="type in questionTypes" :key="type.value" :label="type.label" :value="type.value" />
              </el-select>
            </el-form-item>
            <el-form-item label="Nội dung câu hỏi">
              <el-input v-model="questionForm.content" placeholder="Nhập nội dung câu hỏi" />
            </el-form-item>
            <el-form-item label="Đáp án">
              <el-input v-model="questionForm.answers" placeholder="Nhập đáp án, phân tách bởi dấu ;" />
            </el-form-item>
            <el-form-item label="Media (tùy chọn)">
              <el-upload
                class="media-uploader"
                :show-file-list="false"
                :before-upload="beforeMediaUpload"
                :on-change="handleMediaChange"
                :auto-upload="false"
              >
                <el-icon><UploadFilled /></el-icon>
                <span v-if="questionForm.mediaName">{{ questionForm.mediaName }}</span>
              </el-upload>
            </el-form-item>
            <el-form-item label="Kỹ năng">
              <el-input v-model="questionForm.skill" placeholder="Nhập kỹ năng (ví dụ: Reading, Listening...)" />
            </el-form-item>
            <el-form-item label="Thứ tự">
              <el-input v-model.number="questionForm.order" type="number" min="1" placeholder="Thứ tự câu hỏi" />
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="submitQuestion">Lưu câu hỏi</el-button>
            </el-form-item>
          </el-form>
          <el-table :data="questions" style="margin-top: 20px">
            <el-table-column prop="content" label="Nội dung" />
            <el-table-column prop="type" label="Loại" />
            <el-table-column prop="answers" label="Đáp án" />
            <el-table-column prop="mediaName" label="Media" />
          </el-table>
        </div>

        <el-alert v-if="errorMsg" :title="errorMsg" type="error" show-icon style="margin-top: 20px" />
        <el-alert v-if="successMsg" :title="successMsg" type="success" show-icon style="margin-top: 20px" />
      </div>
    </div>
  </div>
</template>

<script setup>
import UserDashboard from '@/components/UserDashboard.vue';
import { ref } from 'vue';
import axios from 'axios';
import { Plus, UploadFilled } from '@element-plus/icons-vue';

const API_BASE = 'http://localhost:5067';

const step = ref(0);
const courseForm = ref({ title: '', description: '', imageUrl: '', level: '' });
const lessonForm = ref({ title: '', description: '', skill: '', content: '', order: 1 });
const testForm = ref({ title: '', description: '', skill: '', order: 1, duration: 60 });
const questionForm = ref({ questionType: '', content: '', skill: '', order: 1, media: null, mediaName: '', mediaUrl: '' });
const courseId = ref(null);
const lessons = ref([]);
const selectedLessonId = ref(null);
const tests = ref([]);
const selectedTestId = ref(null);
const questions = ref([]);
const errorMsg = ref('');
const successMsg = ref('');
const questionTypes = [
  { value: 'multiple_choice', label: 'Trắc nghiệm' },
  { value: 'fill_blank', label: 'Điền vào chỗ trống' },
  { value: 'essay', label: 'Tự luận' },
  { value: 'listening', label: 'Nghe' },
  { value: 'speaking', label: 'Nói' }
];

function nextStep() {
  step.value++;
  errorMsg.value = '';
  successMsg.value = '';
}
function beforeAvatarUpload(file) {
  const isImage = file.type.startsWith('image/');
  if (!isImage) {
    errorMsg.value = 'Chỉ cho phép tải lên ảnh.';
    return false;
  }
  return true;
}
function handleAvatarChange(file) {
  const reader = new FileReader();
  reader.onload = e => {
    courseForm.value.imageUrl = e.target.result;
  };
  reader.readAsDataURL(file.raw);
}
async function submitCourse() {
  try {
    const now = new Date().toISOString();
    const res = await axios.post(`${API_BASE}/api/Courses`, {
      title: courseForm.value.title,
      description: courseForm.value.description,
      createdAt: now,
      createdBy: 2, // userID mặc định là 2
      rating: 0,
      level: courseForm.value.level || 'Cơ bản',
      imageUrl: courseForm.value.imageUrl
    });
    courseId.value = res.data.id;
    successMsg.value = 'Tạo khóa học thành công!';
    nextStep();
  } catch (err) {
    errorMsg.value = 'Tạo khóa học thất bại.';
  }
}
async function submitLesson() {
  try {
    const res = await axios.post(`${API_BASE}/api/lessons`, {
      courseId: courseId.value,
      skill: lessonForm.value.skill,
      title: lessonForm.value.title,
      content: lessonForm.value.content,
      order: lessonForm.value.order
    });
    lessons.value.push(res.data);
    successMsg.value = 'Thêm bài học thành công!';
    lessonForm.value = { title: '', description: '', skill: '', content: '', order: lessons.value.length + 1 };
  } catch (err) {
    errorMsg.value = 'Thêm bài học thất bại.';
  }
}
async function submitTest() {
  try {
    const res = await axios.post(`${API_BASE}/api/Tests`, {
      lessonId: selectedLessonId.value,
      title: testForm.value.title,
      description: testForm.value.description,
      order: testForm.value.order,
      skill: testForm.value.skill,
      duration: testForm.value.duration
    });
    tests.value.push(res.data);
    successMsg.value = 'Thêm bài kiểm tra thành công!';
    testForm.value = { title: '', description: '', skill: '', order: tests.value.length + 1, duration: 60 };
  } catch (err) {
    errorMsg.value = 'Thêm bài kiểm tra thất bại.';
  }
}
function beforeMediaUpload(file) {
  const isMedia = file.type.startsWith('audio/') || file.type.startsWith('video/') || file.type.startsWith('image/');
  if (!isMedia) {
    errorMsg.value = 'Chỉ cho phép tải lên media (audio, video, ảnh).';
    return false;
  }
  return true;
}
function handleMediaChange(file) {
  questionForm.value.media = file.raw;
  questionForm.value.mediaName = file.name;
}
async function submitQuestion() {
  try {
    let mediaUrl = '';
    let mediaId = null;
    if (questionForm.value.media) {
      const formData = new FormData();
      formData.append('file', questionForm.value.media);
      formData.append('uploader', 2); // uploader mặc định là 2
      const mediaRes = await axios.post(`${API_BASE}/api/media`, formData, {
        headers: { 'Content-Type': 'multipart/form-data' }
      });
      mediaUrl = mediaRes.data.url || mediaRes.data.mediaUrl || '';
      mediaId = mediaRes.data.id;
    }
    const questionRes = await axios.post(`${API_BASE}/api/Questions`, {
      testId: selectedTestId.value,
      content: questionForm.value.content,
      questionType: questionForm.value.questionType,
      skill: questionForm.value.skill,
      mediaUrl: mediaUrl,
      order: questionForm.value.order
    });
    const question = questionRes.data;
    if (mediaId) {
      await axios.post(`${API_BASE}/api/QuestionMedias`, {
        questionId: question.id,
        mediaId: mediaId
      });
    }
    questions.value.push({
      ...question,
      mediaName: questionForm.value.mediaName
    });
    successMsg.value = 'Thêm câu hỏi thành công!';
    questionForm.value = { questionType: '', content: '', skill: '', order: questions.value.length + 1, media: null, mediaName: '', mediaUrl: '' };
  } catch (err) {
    errorMsg.value = 'Thêm câu hỏi thất bại.';
  }
}
</script>

<style scoped>
.add-courses-page {
  background: #f7f8fa;
  min-height: 100vh;
}
.main-content {
  display: flex;
  flex-direction: row;
  margin-top: 60px;
}
.sidebar {
  width: 250px;
  background: #fff;
  border-radius: 16px;
  margin: 20px 20px 20px 0;
  box-shadow: 0 4px 24px rgba(0,0,0,0.10), 0 1.5px 4px rgba(0,0,0,0.04);
  min-height: 600px;
  padding: 32px 0 24px 0;
  display: flex;
  flex-direction: column;
  align-items: center;
}
.sidebar * {
  color: #222 !important;
  font-weight: 500;
}
.sidebar .avatar {
  background: #eaf6ff;
  border: 2px solid #90caf9;
  margin-bottom: 12px;
}
.form-section {
  flex: 1;
  background: #fff;
  border-radius: 12px;
  margin: 20px;
  padding: 32px 40px;
  box-shadow: 0 2px 16px rgba(0,0,0,0.07);
}
.step-form {
  margin-top: 32px;
}
.avatar-uploader, .media-uploader {
  display: flex;
  align-items: center;
  cursor: pointer;
}
.avatar {
  width: 80px;
  height: 80px;
  border-radius: 8px;
  object-fit: cover;
  border: 1px solid #eee;
}
.el-button {
  box-shadow: 0 2px 8px rgba(33,150,243,0.08);
  border-radius: 8px;
  font-weight: 600;
  transition: background 0.2s, color 0.2s;
}
.el-button--primary {
  background: linear-gradient(90deg, #42a5f5 0%, #1976d2 100%);
  border: none;
  color: #fff;
}
.el-button--primary:hover, .el-button--primary:focus {
  background: linear-gradient(90deg, #1976d2 0%, #42a5f5 100%);
  color: #fff;
}
</style>
