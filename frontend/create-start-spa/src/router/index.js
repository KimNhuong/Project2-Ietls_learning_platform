import { createRouter, createWebHistory } from "vue-router";
import HomePage from "@/views/HomePage.vue";
import CoursesPage from "@/views/CourseModule/CoursesPage.vue";
import DictionaryPage from "@/views/DictionaryPage.vue";
import FlashCardPage from "@/views/FlashCardPage.vue";
import LoginPage from "@/views/LoginModule/LoginPage.vue";
import RegistrationPage from "@/views/LoginModule/RegistrationPage.vue";
import ProfilePage from "@/views/UserModule/ProfilePage.vue";
import CourseDetail from "@/views/CourseModule/CourseDetail.vue"; // sửa lại đường dẫn
import Test from "@/views/TestModule/Test.vue";
import TestAttempt from "@/views/TestModule/TestAttempt.vue";
import ChatDeepSeek from "@/views/TestModule/ChatDeepSeek.vue";
import AfterTest from "@/views/TestModule/AfterTest.vue";
import SpeakingTest from "@/views/TestModule/SpeakingTest.vue";
import ScorePage from "@/views/TestModule/ScorePage.vue";
import AddCourses from "@/views/UserModule/AddCourses.vue";

const routes = [
  { path: "/", component: HomePage },
  { path: "/courses", component: CoursesPage },
  { path: "/dictionary", component: DictionaryPage },
  { path: "/flashcard", component: FlashCardPage },
  { path: "/login", component: LoginPage },
  { path: "/register", component: RegistrationPage },
  { path: "/profile", component: ProfilePage },
  {
    path: "/courses/:id",
    name: "CourseDetail",
    component: CourseDetail,
    props: true,
  },
  {
    path: "/Test/:lessonId",
    name: "TestPage",
    component: Test,
    props: true,
  },
  {
    path: "/test-attempt/:testId",
    name: "TestAttempt",
    component: TestAttempt,
    props: true,
  },
  {
    path: "/deepseek-chat",
    name: "ChatDeepSeek",
    component: ChatDeepSeek,
  },
  {
    path: "/after-test",
    name: "AfterTest",
    component: AfterTest,
  },
  {
    path: "/speaking-test",
    name: "SpeakingTest",
    component: SpeakingTest,
  },
  {
    path: "/speaking-test/:testId",
    name: "SpeakingTest",
    component: SpeakingTest,
    props: true,
  },
  {
    path: "/score",
    name: "ScorePage",
    component: ScorePage,
  },
  {
    path: "/add-course",
    name: "AddCourse",
    component: AddCourses,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;