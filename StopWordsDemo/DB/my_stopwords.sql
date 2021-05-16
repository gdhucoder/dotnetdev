/*
 Navicat Premium Data Transfer

 Source Server         : docker-mysql-5004
 Source Server Type    : MySQL
 Source Server Version : 80023
 Source Host           : localhost:5004
 Source Schema         : testdb

 Target Server Type    : MySQL
 Target Server Version : 80023
 File Encoding         : 65001

 Date: 16/05/2021 14:40:56
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for my_stopwords
-- ----------------------------
DROP TABLE IF EXISTS `my_stopwords`;
CREATE TABLE `my_stopwords`  (
  `value` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of my_stopwords
-- ----------------------------
INSERT INTO `my_stopwords` VALUES ('数据');

SET FOREIGN_KEY_CHECKS = 1;
