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

 Date: 16/05/2021 14:40:31
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for word_freq
-- ----------------------------
DROP TABLE IF EXISTS `word_freq`;
CREATE TABLE `word_freq`  (
  `word` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `count` int NULL DEFAULT 1,
  PRIMARY KEY (`word`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
