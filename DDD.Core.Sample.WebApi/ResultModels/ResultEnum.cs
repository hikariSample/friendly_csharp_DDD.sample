using System.ComponentModel;

namespace DDD.Core.Sample.WebApi.ResultModels;

/// <summary>
/// 返回状态枚举
/// </summary>
public enum ResultEnum
{
    /// <summary>
    /// error
    /// </summary>
    [Description("error")]
    ERROR,
    /// <summary>
    /// success
    /// </summary>
    [Description("success")]
    SUCCESS,
    /// <summary>
    /// 未查询到记录
    /// </summary>
    [Description("未查询到记录！")]
    DATA_NOT_EXIST,
    /// <summary>
    /// 参数错误
    /// </summary>
    [Description("参数错误！")]
    PARAM_ERROR,
    /// <summary>
    /// 已存在同级同名的分类
    /// </summary>
    [Description("已存在同级同名的分类！")]
    SAME_CATEGORY_EXIST,
    /// <summary>
    /// 用户名已存在
    /// </summary>
    [Description("用户名已存在！")]
    SAME_LOGIN_NAME_EXIST,
    /// <summary>
    /// 手机号已存在
    /// </summary>
    [Description("手机号已存在！")]
    SAME_PHONE_EXIST,
    /// <summary>
    /// 请输入登录名
    /// </summary>
    [Description("请输入登录名！")]
    LOGIN_NAME_NULL,
    /// <summary>
    /// 请输入正确的手机号
    /// </summary>
    [Description("请输入正确的手机号！")]
    LOGIN_NAME_IS_NOT_PHONE,
    /// <summary>
    /// 请输入密码
    /// </summary>
    [Description("请输入密码！")]
    LOGIN_PASSWORD_NULL,
    /// <summary>
    /// 请输入验证码
    /// </summary>
    [Description("请输入验证码！")]
    LOGIN_VERIFY_CODE_NULL,
    /// <summary>
    /// 验证码错误
    /// </summary>
    [Description("验证码错误！")]
    LOGIN_VERIFY_CODE_ERROR,
    /// <summary>
    /// 已存在相同的首页配置项
    /// </summary>
    [Description("已存在相同的首页配置项！")]
    SAME_INDEX_CONFIG_EXIST,
    /// <summary>
    /// 分类数据异常
    /// </summary>
    [Description("分类数据异常！")]
    GOODS_CATEGORY_ERROR,
    /// <summary>
    /// 已存在相同的商品信息
    /// </summary>
    [Description("已存在相同的商品信息！")]
    SAME_GOODS_EXIST,
    /// <summary>
    /// 商品不存在
    /// </summary>
    [Description("商品不存在！")]
    GOODS_NOT_EXIST,
    /// <summary>
    /// 商品已下架
    /// </summary>
    [Description("商品已下架！")]
    GOODS_PUT_DOWN,
    /// <summary>
    /// 超出单个商品的最大购买数量
    /// </summary>
    [Description("超出单个商品的最大购买数量！")]
    SHOPPING_CART_ITEM_LIMIT_NUMBER_ERROR,
    /// <summary>
    /// 商品数量不能小于 1
    /// </summary>
    [Description("商品数量不能小于 1 ！")]
    SHOPPING_CART_ITEM_NUMBER_ERROR,
    /// <summary>
    /// 超出购物车最大容量
    /// </summary>
    [Description("超出购物车最大容量！")]
    SHOPPING_CART_ITEM_TOTAL_NUMBER_ERROR,
    /// <summary>
    /// 已存在！无需重复添加
    /// </summary>
    [Description("已存在！无需重复添加！")]
    SHOPPING_CART_ITEM_EXIST_ERROR,
    /// <summary>
    /// 登录失败
    /// </summary>
    [Description("登录失败！")]
    LOGIN_ERROR,
    /// <summary>
    /// 未登录
    /// </summary>
    [Description("未登录！")]
    NOT_LOGIN_ERROR,
    /// <summary>
    /// 管理员未登录
    /// </summary>
    [Description("管理员未登录！")]
    ADMIN_NOT_LOGIN_ERROR,
    /// <summary>
    /// 无效认证！请重新登录
    /// </summary>
    [Description("无效认证！请重新登录！")]
    TOKEN_EXPIRE_ERROR,
    /// <summary>
    /// 管理员登录过期！请重新登录
    /// </summary>
    [Description("管理员登录过期！请重新登录！")]
    ADMIN_TOKEN_EXPIRE_ERROR,
    /// <summary>
    /// 无效用户！请重新登录
    /// </summary>
    [Description("无效用户！请重新登录！")]
    USER_NULL_ERROR,
    /// <summary>
    /// 用户已被禁止登录
    /// </summary>
    [Description("用户已被禁止登录！")]
    LOGIN_USER_LOCKED_ERROR,
    /// <summary>
    /// 订单不存在
    /// </summary>
    [Description("订单不存在！")]
    ORDER_NOT_EXIST_ERROR,
    /// <summary>
    /// 订单项不存在
    /// </summary>
    [Description("订单项不存在！")]
    ORDER_ITEM_NOT_EXIST_ERROR,
    /// <summary>
    /// 地址不能为空
    /// </summary>
    [Description("地址不能为空！")]
    NULL_ADDRESS_ERROR,
    /// <summary>
    /// 订单价格异常
    /// </summary>
    [Description("订单价格异常！")]
    ORDER_PRICE_ERROR,
    /// <summary>
    /// 订单项异常
    /// </summary>
    [Description("订单项异常！")]
    ORDER_ITEM_NULL_ERROR,
    /// <summary>
    /// 生成订单异常
    /// </summary>
    [Description("生成订单异常！")]
    ORDER_GENERATE_ERROR,
    /// <summary>
    /// 购物车数据异常
    /// </summary>
    [Description("购物车数据异常！")]
    SHOPPING_ITEM_ERROR,
    /// <summary>
    /// 库存不足
    /// </summary>
    [Description("库存不足！")]
    SHOPPING_ITEM_COUNT_ERROR,
    /// <summary>
    /// 订单状态异常
    /// </summary>
    [Description("订单状态异常！")]
    ORDER_STATUS_ERROR,
    /// <summary>
    /// 操作失败
    /// </summary>
    [Description("操作失败！")]
    OPERATE_ERROR,
    /// <summary>
    /// 禁止该操作
    /// </summary>
    [Description("禁止该操作！")]
    REQUEST_FORBIDEN_ERROR,
    /// <summary>
    /// 无权限
    /// </summary>
    [Description("无权限！")]
    NO_PERMISSION_ERROR,
    /// <summary>
    /// database error
    /// </summary>
    [Description("database error")]
    DB_ERROR
}